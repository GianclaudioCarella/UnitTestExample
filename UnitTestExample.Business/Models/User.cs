using System;
using System.Collections.Generic;

namespace UnitTestExample.Business.Models
{
    public class User
    {
        private Guid _userId;
        public Guid UserId 
        {
            get => _userId;
            set => _userId = value;
        }

        private string _name;
        public string Name 
        {
            get => _name;
            set => _name = value; 
        }

        private string _email;
        public string Email
        {
            get => _email;
            set => _email = value;
        }


        private List<Trip> _trips;
        public List<Trip> Trips 
        { 
            get => _trips;
            set => _trips = value;
        }

        private double _total;
        public double Total
        {
            get => _total;
            set => _total = value; 
        }

        public User()
        {

        }
    }
}
