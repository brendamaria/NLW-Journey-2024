using Journey.Exception.ExceptionsBase;
using Journey.Exception;
using Journey.Infrastructure;

namespace Journey.Application.UseCases.Activities.Delete
{
    public class DeleteActivityUseCase
    {
        public void Execute(Guid activityId, Guid tripId)
        {
            var dbContext = new JourneyDbContext();

            var activity = dbContext.Activities
                .FirstOrDefault(a => a.Id == activityId && a.TripId == tripId);

            if (activity is null)
            {
                throw new NotFoundException(ResourceErrorMessages.ACTIVITY_NOT_FOUND);
            }

            dbContext.Activities.Remove(activity);
            dbContext.SaveChanges();
        }
    }
}
