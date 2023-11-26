using Codem.Infrastructure.Common;
using NHibernate.Event;

namespace Codem.Infrastructure.Listeners;

public class SqlChangeDtListener : BaseListener, IPreUpdateEventListener
{
    public bool OnPreUpdate(PreUpdateEvent @event)
    {
        if (@event.Entity is not SqlEntity entity)
            return false;
            
        DateTime now = DateTime.UtcNow;
        entity.ChangeDt = now;
            
        Set(@event.Persister, @event.State, "ChangeDt", entity.ChangeDt);
            
        return false;
    }
    
    public Task<bool> OnPreUpdateAsync(PreUpdateEvent @event, CancellationToken cancellationToken)
    {
        return Task.FromResult(OnPreUpdate(@event));
    }
}