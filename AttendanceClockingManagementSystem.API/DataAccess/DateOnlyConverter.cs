﻿using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AttendanceClockingManagementSystem.API.DataAccess
{
    public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
    {
        /// <summary>
        /// Creates a new instance of this converter.
        /// </summary>
        public DateOnlyConverter() : base(
                d => d.ToDateTime(TimeOnly.MinValue),
                d => DateOnly.FromDateTime(d))
        { }
    }

   
}
