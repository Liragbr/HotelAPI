using Microsoft.AspNetCore.Mvc;
using ReservationsAPI.Models;
using System;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private static List<Reservation> reservations = new List<Reservation>();

    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> GetReservations()
    {
        return Ok(reservations);
    }

    [HttpGet("{id}")]
    public ActionResult<Reservation> GetReservation(int id)
    {
        var reservation = reservations.Find(r => r.Id == id);
        if (reservation == null)
        {
            return NotFound();
        }
        return Ok(reservation);
    }

    [HttpPost]
    public ActionResult<Reservation> CreateReservation([FromBody] Reservation reservation)
    {
        reservation.Id = reservations.Count + 1;
        reservations.Add(reservation);
        return CreatedAtAction(nameof(GetReservation), new { id = reservation.Id }, reservation);
    }

    [HttpPut("{id}")]
    public ActionResult UpdateReservation(int id, [FromBody] Reservation reservation)
    {
        var existingReservation = reservations.Find(r => r.Id == id);
        if (existingReservation == null)
        {
            return NotFound();
        }

        existingReservation.GuestName = reservation.GuestName;
        existingReservation.CheckInDate = reservation.CheckInDate;
        existingReservation.CheckOutDate = reservation.CheckOutDate;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteReservation(int id)
    {
        var existingReservation = reservations.Find(r => r.Id == id);
        if (existingReservation == null)
        {
            return NotFound();
        }

        reservations.Remove(existingReservation);
        return NoContent();
    }
}
