//using System;
//using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace mvc_crud.Models
{
    // Modelos devueltos por las acciones de AccountController.
    public class DriverModel
    {
        public int Id{ get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public int Age { get; set; }
        public bool Active { get; set; }
    }
}