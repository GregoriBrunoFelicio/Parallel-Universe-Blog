
namespace Parallel.Universe.Blog.Api.Entities
{
    public abstract class Entity
    {
        protected Entity()
        {

        }

        public virtual int Id { get; }

        protected Entity(int id) => Id = id;
    }
}
