namespace Core.BaseModels
{
    public interface IBaseModel
    {
        public int Id { get; set; }

        object CopyFrom(object newObject);
        string WhatChanged(object newObject);
        string ToLogString();
    }
}
