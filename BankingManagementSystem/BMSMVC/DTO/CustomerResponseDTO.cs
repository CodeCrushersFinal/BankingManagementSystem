﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BMSMVC.DTO
{
    public class CustomerResponseDTO
    {
        public int CustomerId {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email {  get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; } 
        public string PassswordHash {  get; set; }
        public int RoleId {  get; set; }

    }
}