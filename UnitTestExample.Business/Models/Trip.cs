using System;
using System.Collections.Generic;

namespace UnitTestExample.Business.Models
{
    public class Trip
    {
        private Guid _tripId;
        public Guid TripId 
        {
            get => _tripId;
            set => _tripId = value;
        }

        private string _location;
        public string Location 
        {
            get => _location;
            set => _location = value;
        }

        private List<Bill> _bills;
        public List<Bill> Bills 
        {
            get => _bills;
            set => _bills = value;
        }

    }

}