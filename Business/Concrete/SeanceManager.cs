using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

public class SeanceManager
{
    private readonly ISeanceDal _seanceDal;

    public SeanceManager(ISeanceDal seanceDal)
    {
        _seanceDal = seanceDal;
    }

    public void Add(Seance seance)
    {
        _seanceDal.Add(seance);
    }

    public void Delete(Seance entity)
    {
        _seanceDal.Delete(entity);
    }

    public void Delete(int id)
    {
        var seanceId= _seanceDal.GetById(id);
        _seanceDal.Delete(seanceId);
    }

    public List<Seance> GetAll()
    {
        return _seanceDal.List();
    }
    public List<Seance> GetAllT(Expression<Func<Seance, bool>> filter = null, params string[] includeProperties)
    {
        return _seanceDal.GetAllIncluding(filter, "Movie", "MovieTheater").ToList();
    }


    public Seance GetById(int id)
    {
        return _seanceDal.Get(m => m.Id == id, includeProperties: "Movie,MovieTheater");
    }

    public List<Seance> GetSeancesForMovie(int movieId)
    {
        var seances = _seanceDal.GetAllIncluding(x => x.MovieId == movieId, "Movie", "MovieTheater").ToList();
        return seances ?? new List<Seance>();
    }

    public void Update(Seance seance)
    {
        Seance resultSeance = _seanceDal.GetById(x=>x.Id == seance.Id);
        resultSeance.MovieId = seance.MovieId;
        resultSeance.MovieTheaterId = seance.MovieTheaterId; 
        resultSeance.Hours = seance.Hours;
        resultSeance.Date = seance.Date;
        resultSeance.SeatsOccupied = seance.SeatsOccupied;
        resultSeance.Status = seance.Status;
        _seanceDal.Update(resultSeance);
    }

    public void UpdateOccupiedSeats(int seanceId, List<string> selectedSeats)
    {
        var seance = _seanceDal.GetById(seanceId);
        if (seance != null)
        {
            var occupiedSeats = seance.SeatsOccupied?.Split(',').ToList() ?? new List<string>();
            occupiedSeats.AddRange(selectedSeats);
            seance.SeatsOccupied = string.Join(',', occupiedSeats.Distinct());

            _seanceDal.Update(seance);
        }
    }
}
