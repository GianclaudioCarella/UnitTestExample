using System;
using System.Collections.Generic;
using System.Linq;
using UnitTestExample.Business.Data;
using UnitTestExample.Business.Models;

namespace UnitTestExample.Business.Services
{
    public class BillService
    {
        private ApplicationDbContext _context;

        public BillService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the trip bills.
        /// </summary>
        /// <returns>The bills.</returns>
        /// <param name="tripId">Trip id.</param>
        public IEnumerable<Bill> GetBills(Guid tripId)
        {
            return _context.Bills.Where(b => b.TripId == tripId).ToList();
        }

        #region CRUD Bill

        /// <summary>
        /// Gets the bill.
        /// </summary>
        /// <returns>The bill.</returns>
        /// <param name="Id">Identifier.</param>
        public Bill Get(Guid Id)
        {
            return _context.Bills.Find(Id);
        }

        /// <summary>
        /// Add the specified bill.
        /// </summary>
        /// <returns>The add.</returns>
        /// <param name="bill">Bill.</param>
        public Result Add(Bill bill)
        {
            var result = ValidateBill(bill);
            result.Action = "Add new Bill";

            if (result.Validations.Count == 0)
            {
                _context.Bills.Add(bill);
                SplitBill(bill);
                _context.SaveChanges();
            }

            return result;
        }

        /// <summary>
        /// Update the specified bill.
        /// </summary>
        /// <returns>The update.</returns>
        /// <param name="bill">Bill.</param>
        public Result Update(Bill bill)
        {
            Result result = ValidateBill(bill);
            result.Action = "Update the Bill";

            if (result.Validations.Count == 0)
            {
                Bill pobjBill = _context.Bills.Find(bill.BillId);

                if (pobjBill == null)
                {
                    result.Validations.Add("Bill not exists");
                }
                else
                {
                    pobjBill.Debit = bill.Debit;
                    pobjBill.Users = bill.Users;
                    _context.SaveChanges();
                }
            }

            return result;
        }

        /// <summary>
        /// Delete the specified bill.
        /// </summary>
        /// <returns>The delete.</returns>
        /// <param name="bill">Bill.</param>
        public Result Delete(Guid billId)
        {
            var result = new Result
            {
                Action = "Delete a bill"
            };

            Bill pobjBill = _context.Bills.Find(billId);

            if (pobjBill == null)
            {
                result.Validations.Add("Bill not exists");
            }
            else
            {
                _context.Bills.Remove(pobjBill);
                _context.SaveChanges();
            }

            return result;
        }

        #endregion

        /// <summary>
        /// Validates the bill.
        /// </summary>
        /// <returns>The bill.</returns>
        /// <param name="bill">Bill.</param>
        public Result ValidateBill(Bill bill)
        {
            var result = new Result();

            if (bill.Debit <= 0)
                result.Validations.Add("Please inform the bill value.");

            if (bill.Users.Count < 1)
                result.Validations.Add("The bill needs a User.");

            return result;
        }

        /// <summary>
        /// Split a bill and increase the users total account
        /// </summary>
        /// <param name="bill">Bill.</param>
        public void SplitBill(Bill bill)
        {
            double total = bill.Debit / bill.Users.Count;
            foreach (var item in bill.Users)
            {
                var user = _context.Users.Find(item.UserId);
                user.Total += total;
                _context.SaveChanges();
            }
        }



    }
}
