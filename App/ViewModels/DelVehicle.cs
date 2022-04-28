using System.ComponentModel.DataAnnotations;
using App.Models;
using App.ViewModels.CustomDataAnnotation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.ViewModels
{
    public class DelVehicle
    {
        public DelVehicle()
        {
            VehiclesList = new List<Itemlist>();
        }
        public DelVehicle(ICollection<Vehicle> vehicles)
        {
            VehiclesList = new List<Itemlist>(vehicles.Count);
            foreach (Vehicle vehicle in vehicles)
            {
                VehiclesList.Add(new Itemlist { Value = vehicle.Id, Text = vehicle.Brand + " " + vehicle.Model });
            }
        }
        public int VehicleId { get; set; }
        public List<Itemlist> VehiclesList { get; set; }
    }
}