﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DotNetCore_New.Data.Config
{
    public class StudentConfig: IEntityTypeConfiguration<Student>   
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(x => x.StudentId);

            builder.Property(x => x.StudentId).UseIdentityColumn();

            builder.Property(n => n.StudentName).IsRequired().HasMaxLength(250);
            builder.Property(n => n.StudentEmail).IsRequired().HasMaxLength(250);
            builder.Property(n => n.StudentPhone).IsRequired(false).HasMaxLength(15);

            builder.HasData(new List<Student>()
            {
                new Student
                {
                    StudentId = 1,
                    StudentName = "Saket Jha",
                    StudentEmail = "saketkumar180@gmail.com",
                    StudentPhone = "9177881115",
                    DOB = new DateTime(1995,3,11)
                },
                new Student
                {
                    StudentId = 2,
                    StudentName = "Subham singh",
                    StudentEmail = "subhamsingh@gmail.com",
                    StudentPhone = "8437074075",
                    DOB = new DateTime(1994,11,18)
                }
            });
        }
    }
}