using System;
using System.Collections.Generic;
using DataAccess.DataModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.EmpDbContext;

public partial class SqlPracticeContext : DbContext
{
    public SqlPracticeContext()
    {
    }

    public SqlPracticeContext(DbContextOptions<SqlPracticeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<DbLog> DbLogs { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeeTemp> EmployeeTemps { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<Test1> Test1s { get; set; }

    public virtual DbSet<Test2> Test2s { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAuth> UserAuths { get; set; }

    public virtual DbSet<VwEmployee> VwEmployees { get; set; }

    public virtual DbSet<VwEmployeeandDept> VwEmployeeandDepts { get; set; }

    public virtual DbSet<VwGetAllUser> VwGetAllUsers { get; set; }

    public virtual DbSet<VwNoOfEmpPerDept> VwNoOfEmpPerDepts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:practiceDB");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.ToTable("City");

            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.CityName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.StateId).HasColumnName("StateID");

            entity.HasOne(d => d.State).WithMany(p => p.Cities)
                .HasForeignKey(d => d.StateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_City_City");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("Country");

            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.CountryName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DbLog>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.AuditDateTime).HasColumnType("datetime");
            entity.Property(e => e.Dbname)
                .HasMaxLength(250)
                .HasColumnName("DBName");
            entity.Property(e => e.EventType).HasMaxLength(250);
            entity.Property(e => e.LoginName).HasMaxLength(250);
            entity.Property(e => e.SqlQuery).HasMaxLength(250);
            entity.Property(e => e.TableName).HasMaxLength(250);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Department");

            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee", tb =>
                {
                    tb.HasTrigger("TR_CheckEmpDetails");
                    tb.HasTrigger("TR_NewUserOnNewEmpReg");
                });

            entity.Property(e => e.Address)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.DateofBirth).HasColumnType("datetime");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MailSentDate).HasColumnType("datetime");
            entity.Property(e => e.PinCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Salary).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.City).WithMany(p => p.Employees)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK_Employee_City");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK_Employee_Department");
        });

        modelBuilder.Entity<EmployeeTemp>(entity =>
        {
            entity.HasKey(e => e.EmployeeId);

            entity.ToTable("EmployeeTemp");

            entity.Property(e => e.EmployeeId).ValueGeneratedNever();
            entity.Property(e => e.Address)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.DateofBirth).HasColumnType("datetime");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PinCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Salary).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Message)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.ToTable("State");

            entity.Property(e => e.StateId).HasColumnName("StateID");
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.StateName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Country).WithMany(p => p.States)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK_State_Country");
        });

        modelBuilder.Entity<Test1>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("test1");

            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<Test2>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("test2");

            entity.Property(e => e.Id).HasColumnName("id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Address)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.DateofBirth).HasColumnType("datetime");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PinCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Salary).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<UserAuth>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_User");

            entity.ToTable("UserAuth");

            entity.Property(e => e.UserId).HasColumnName("User_Id");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("First_Name");
            entity.Property(e => e.Password)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .HasColumnName("Phone_Number");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwEmployee>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_Employee");

            entity.Property(e => e.Address)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.DateofBirth).HasColumnType("datetime");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.EmployeeId).ValueGeneratedOnAdd();
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PinCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Salary).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<VwEmployeeandDept>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_EmployeeandDept");

            entity.Property(e => e.Address)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.DateofBirth).HasColumnType("datetime");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PinCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Salary).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<VwGetAllUser>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_GetAllUsers");

            entity.Property(e => e.Address)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.CityId).HasColumnName("CityID");
            entity.Property(e => e.DateofBirth).HasColumnType("datetime");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PinCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Salary).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.UserId).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<VwNoOfEmpPerDept>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_NoOfEmpPerDept");

            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
