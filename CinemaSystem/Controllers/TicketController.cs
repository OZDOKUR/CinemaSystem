using Business.Concrete;
using CinemaSystem.Models;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

public class TicketController : Controller
{
    private readonly SeanceManager _seanceManager;
    private readonly MovieManager _movieManager;
    private readonly TicketManager _ticketManager;
    

    public TicketController()
    {
        _seanceManager = new SeanceManager(new EfSeanceDal());
        _movieManager = new MovieManager(new EfMovieDal());
        _ticketManager = new TicketManager(new EfTicketDal());
    
    }

    public IActionResult Reserve(string name)
    {
        int movieId = _movieManager.GetIdByName(name);
        var seances = _seanceManager.GetSeancesForMovie(movieId);
        return View(seances);
    }

    [HttpGet]
    public IActionResult SeatPlan(int seanceId)
    {
        var seance = _seanceManager.GetById(seanceId);
        if (seance == null)
        {
            return NotFound();
        }
        if (seance.Status == false)
        {
            return RedirectToAction ("Index","home");
        }
        else
        {
            var model = new SeatPlanViewModel
            {
                SeanceId = seanceId,
                SelectedSeats = "",
                OccupiedSeats = seance.SeatsOccupied ?? "",
                TotalPrice = 0
            };
            return View(model);
        }
        
    }

    [HttpPost]
    public IActionResult SeatPlan(SeatPlanViewModel model)
    {
        var selectedSeatsList = model.SelectedSeats.Split(',').ToList();

        
        var seance = _seanceManager.GetById(model.SeanceId);
        if (seance == null)
        {
            return NotFound();
        }

        
        var currentOccupiedSeats = seance.SeatsOccupied?.Split(',').ToList() ?? new List<string>();
        currentOccupiedSeats.AddRange(selectedSeatsList);
        seance.SeatsOccupied = string.Join(",", currentOccupiedSeats.Distinct());


       
        _seanceManager.Update(seance);

      
        var totalPrice = selectedSeatsList.Count * 100; 

        
        var ticket = new Ticket
        {
            SeanceId = model.SeanceId,
            TicketPrice = totalPrice, 
            UserId = 1, //
            WorkerId = 2, 
            SellTime = DateTime.Now
        };
        _ticketManager.Add(ticket);

        
        return RedirectToAction("Index","Home", new { selectedSeats = model.SelectedSeats, totalPrice });
    }

    public IActionResult ShowSelectedSeats(string selectedSeats, decimal totalPrice)
    {
        var model = new SeatPlanViewModel
        {
            SelectedSeats = selectedSeats,
            TotalPrice = totalPrice
        };
        return View(model);
    }

    [HttpPost]
    public IActionResult Payment(SeatPlanViewModel model)
    {
       
        return View(model);
    }
}
