using System;
using System.Linq;
using UnitTestExample.Business.Data;
using UnitTestExample.Business.Models;

namespace UnitTestExample.Business.Services
{
    public class TripService
    {
        private ApplicationDbContext _context;

        public TripService(ApplicationDbContext context)
        {
            _context = context;
        }

        #region CRUD Trip
        /// <summary>
        /// Add the specified trip.
        /// </summary>
        /// <returns>The add.</returns>
        /// <param name="trip">Trip.</param>
        public Result Add(Trip trip)
        {

            Result result = ValidateTrip(trip);
            result.Action = "Add new Trip";

            if(result.Validations.Count == 0)
            {
                _context.Trips.Add(trip);
                _context.SaveChanges();
            }

            return result;
        }

        /// <summary>
        /// Get the specified id.
        /// </summary>
        /// <returns>The get.</returns>
        /// <param name="id">Identifier.</param>
        public Trip Get(Guid id)
        {
            return _context.Trips.Find(id);
        }

        /// <summary>
        /// Update the specified trip.
        /// </summary>
        /// <returns>The update.</returns>
        /// <param name="trip">Trip.</param>
        public Result Update(Trip trip)
        {
            Result result = ValidateTrip(trip);
            result.Action = "Update the Trip";

            if(result.Validations.Count == 0)
            {
                Trip pobjTrip = _context.Trips.Find(trip.TripId);

                if(pobjTrip == null)
                {
                    result.Validations.Add("Trip not exists");
                }
                else
                {
                    pobjTrip.Location = trip.Location;
                    _context.SaveChanges();
                }
            }

            return result;
        }

        /// <summary>
        /// Delete the specified tripId.
        /// </summary>
        /// <returns>The delete.</returns>
        /// <param name="tripId">Trip identifier.</param>
        public Result Delete(Guid tripId)
        {
            Result result = new Result
            {
                Action = "Delete a trip"
            };

            Trip pobjTrip = _context.Trips.Find(tripId);

            if(pobjTrip == null)
            {
                result.Validations.Add("Trip not exists");
            }
            else
            {
                _context.Trips.Remove(pobjTrip);
                _context.SaveChanges();
            }            

            return result;
        }

        #endregion

        /// <summary>
        /// Validations to create a new trip
        /// </summary>
        /// <returns>The validation.</returns>
        public Result ValidateTrip(Trip trip)
        {
            var result = new Result();

            if (String.IsNullOrEmpty(trip.Location) && String.IsNullOrWhiteSpace(trip.Location))
                result.Validations.Add("The trip needs a location");

            return result;
        }
        
    }
}
