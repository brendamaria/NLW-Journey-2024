using FluentValidation.Results;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception;
using Journey.Exception.ExceptionsBase;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Activities.Register
{
    public class RegisterActivityForTripUseCase
    {
        public ResponseActivityJson Execute(Guid tripId, RequestRegisterActivityJson request)
        {
            var dbContext = new JourneyDbContext();
            var trip = dbContext.Trips.FirstOrDefault(t => t.Id == tripId);

            if(trip == null)
            {
                throw new NotFoundException(ResourceErrorMessages.TRIP_NOT_FOUND);
            }

            Validate(trip, request);

            var entity = new Activity
            { 
                Date = request.Date,
                Name = request.Name,
                TripId = tripId,
            };

            dbContext.Activities.Add(entity);
            dbContext.SaveChanges();

            return new ResponseActivityJson()
            {
                Id = entity.Id,
                Name = entity.Name,
                Date = entity.Date,
                Status = (Communication.Enums.ActivityStatus)entity.Status,
            };
        }

        private void Validate(Trip trip, RequestRegisterActivityJson request)
        {
            var validator = new RegisterActivityValidator();
            var result = validator.Validate(request);

            if(request.Date < trip.StartDate || request.Date > trip.EndDate)
            {
                result.Errors.Add(new ValidationFailure("Date", ResourceErrorMessages.INVALID_ACTIVITY_DATE));
            }

            if (result.IsValid == false)
            {
                var error = result.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(error);
            }

        }
    }
}
