﻿namespace Domain.Exercise
{
    public class Musclegroup : IMusclegroup
    {
        public Musclegroup()
        {

        }
        public Musclegroup(int id, string name)
        {
            _id = id;
            _name = name;
        }
        private int _id;
        private string? _name;

        public int Id
        {
            get => _id; set => _id = value;
        }
        public string Name
        {
            get => _name ??= ""; set => _name = value;
        }
    }
}
