using Codem.Infrastructure.Common;
using NHibernate.Event;

namespace Codem.Infrastructure.Listeners;

public class SqlCreateDtListener : BaseListener, IPreInsertEventListener
{
    public bool OnPreInsert(PreInsertEvent @event)
    {
        if (@event.Entity is not SqlEntity entity)
            return false;
        
        DateTime now = DateTime.UtcNow;
        entity.CreateDt = now;
        entity.ChangeDt = now;

        Set(@event.Persister, @event.State, "CreateDt", entity.CreateDt);
        Set(@event.Persister, @event.State, "ChangeDt", entity.ChangeDt);
            
        return false;
    }

    public Task<bool> OnPreInsertAsync(PreInsertEvent @event, CancellationToken cancellationToken)
    {
        return Task.FromResult(OnPreInsert(@event));
    }
}