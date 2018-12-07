using System;

namespace RssReader.Models
{
    /// <summary>
    /// Интерфейс для классов, которые необходимо создавать и редактировать.
    /// Такие как:
    /// Проект(Объект) (<see cref="Project"/>) или
    /// Свая (<see cref="Pile"/>)
    /// </summary>
    public interface ICreatable : IComparable, ICloneable
    {
        /// <summary>Флаг валидности всех свойств объекта</summary>
        bool IsValid { get; }
    }
}
