namespace Contracts
{
    using Entities.Models;

    public interface IDataShaper<T>
    {
        IEnumerable<ShapedEntity> ShapeData(IEnumerable<T> entities, string? fieldsString);
        ShapedEntity ShapeData(T entity, string? fieldsString);
    }
}
