using System;
using System.Collections.Generic;

namespace UnitTestExample.Business.Models
{
    public class Bill
    {
        private Guid _billId;
        public Guid BillId
        {
            get => _billId;
            set => _billId = value;
        }

        private Guid _tripId;
        public Guid TripId
        {
            get => _tripId;
            set => _tripId = value;
        }


        private double _debit;
        public double Debit
        {
            get => _debit;
            set => _debit = value;
        }

        private List<User> _users;
        public List<User> Users
        {
            get => _users;
            set => _users = value;
        }
    }
}