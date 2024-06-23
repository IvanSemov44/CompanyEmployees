﻿namespace Contracts
{
    using System.Dynamic;

    public interface IDataShaper<T>
    {
        IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string? fieldsString);
        ExpandoObject ShapeData(T entity, string? fieldsString);
    }
}
