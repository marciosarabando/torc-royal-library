namespace Domain.Entities.Base;
public abstract class BaseEntity
{
    public BaseEntity()
    {
        CreatedAt = DateTime.Now;
        Active = true;
    }

    public int Id { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastUpdate { get; set; }

    public void Update()
    {
        LastUpdate = DateTime.Now;
    }

    public void Activate()
    {
        Active = true;
    }

    public void Deactivate()
    {
        Update();
        Active = false;
    }
}