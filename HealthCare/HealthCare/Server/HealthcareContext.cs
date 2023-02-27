using System;
using System.Collections.Generic;
using HealthCare.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace HealthCare.Server;

public partial class HealthcareContext : DbContext
{
    public HealthcareContext()
    {
    }

    public HealthcareContext(DbContextOptions<HealthcareContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<Disease> Diseases { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Drug> Drugs { get; set; }

    public virtual DbSet<LabResult> LabResults { get; set; }

    public virtual DbSet<LabTechnician> LabTechnicians { get; set; }

    public virtual DbSet<LabTest> LabTests { get; set; }

    public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }

    public virtual DbSet<Nurse> Nurses { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Prescription> Prescriptions { get; set; }

    public virtual DbSet<SoldDrug> SoldDrugs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.HasKey(e => e.AppointmentId).HasName("PRIMARY");

            entity.ToTable("appointments");

            entity.HasIndex(e => e.DoctorId, "doctor_id");

            entity.HasIndex(e => e.NurseId, "nurse_id");

            entity.HasIndex(e => e.PatientId, "patient_id");

            entity.Property(e => e.AppointmentId)
                .HasColumnType("int(11)")
                .HasColumnName("appointment_id");
            entity.Property(e => e.AppointmentDate)
                .HasColumnType("datetime")
                .HasColumnName("appointment_date");
            entity.Property(e => e.DoctorId)
                .HasColumnType("int(11)")
                .HasColumnName("doctor_id");
            entity.Property(e => e.NurseId)
                .HasColumnType("int(11)")
                .HasColumnName("nurse_id");
            entity.Property(e => e.PatientId)
                .HasColumnType("int(11)")
                .HasColumnName("patient_id");

            entity.HasOne(d => d.Doctor).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("appointments_ibfk_2");

            entity.HasOne(d => d.Nurse).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.NurseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("appointments_ibfk_3");

            entity.HasOne(d => d.Patient).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("appointments_ibfk_1");
        });

        modelBuilder.Entity<Disease>(entity =>
        {
            entity.HasKey(e => e.DiseaseId).HasName("PRIMARY");

            entity.ToTable("diseases");

            entity.Property(e => e.DiseaseId)
                .HasColumnType("int(11)")
                .HasColumnName("disease_id");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.DiseaseName)
                .HasMaxLength(50)
                .HasColumnName("disease_name");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PRIMARY");

            entity.ToTable("doctors");

            entity.Property(e => e.DoctorId)
                .HasColumnType("int(11)")
                .HasColumnName("doctor_id");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .HasColumnName("phone_number");
            entity.Property(e => e.Specialization)
                .HasMaxLength(100)
                .HasColumnName("specialization");
        });

        modelBuilder.Entity<Drug>(entity =>
        {
            entity.HasKey(e => e.DrugId).HasName("PRIMARY");

            entity.ToTable("drugs");

            entity.Property(e => e.DrugId)
                .HasColumnType("int(11)")
                .HasColumnName("drug_id");
            entity.Property(e => e.Dosage)
                .HasMaxLength(50)
                .HasColumnName("dosage");
            entity.Property(e => e.DrugName)
                .HasMaxLength(50)
                .HasColumnName("drug_name");
            entity.Property(e => e.Manufacturer)
                .HasMaxLength(50)
                .HasColumnName("manufacturer");
            entity.Property(e => e.UnitPrice)
                .HasPrecision(10, 2)
                .HasColumnName("unit_price");
        });

        modelBuilder.Entity<LabResult>(entity =>
        {
            entity.HasKey(e => e.ResultId).HasName("PRIMARY");

            entity.ToTable("lab_results");

            entity.HasIndex(e => e.PatientId, "patient_id");

            entity.HasIndex(e => e.TechnicianId, "technician_id");

            entity.HasIndex(e => e.TestId, "test_id");

            entity.Property(e => e.ResultId)
                .HasColumnType("int(11)")
                .HasColumnName("result_id");
            entity.Property(e => e.PatientId)
                .HasColumnType("int(11)")
                .HasColumnName("patient_id");
            entity.Property(e => e.ResultDate)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("result_date");
            entity.Property(e => e.ResultText)
                .HasColumnType("text")
                .HasColumnName("result_text");
            entity.Property(e => e.TechnicianId)
                .HasColumnType("int(11)")
                .HasColumnName("technician_id");
            entity.Property(e => e.TestId)
                .HasColumnType("int(11)")
                .HasColumnName("test_id");

            entity.HasOne(d => d.Patient).WithMany(p => p.LabResults)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lab_results_ibfk_3");

            entity.HasOne(d => d.Technician).WithMany(p => p.LabResults)
                .HasForeignKey(d => d.TechnicianId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lab_results_ibfk_2");

            entity.HasOne(d => d.Test).WithMany(p => p.LabResults)
                .HasForeignKey(d => d.TestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("lab_results_ibfk_1");
        });

        modelBuilder.Entity<LabTechnician>(entity =>
        {
            entity.HasKey(e => e.TechnicianId).HasName("PRIMARY");

            entity.ToTable("lab_technicians");

            entity.Property(e => e.TechnicianId)
                .HasColumnType("int(11)")
                .HasColumnName("technician_id");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .HasColumnName("phone_number");
        });

        modelBuilder.Entity<LabTest>(entity =>
        {
            entity.HasKey(e => e.TestId).HasName("PRIMARY");

            entity.ToTable("lab_tests");

            entity.Property(e => e.TestId)
                .HasColumnType("int(11)")
                .HasColumnName("test_id");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.TestName)
                .HasMaxLength(50)
                .HasColumnName("test_name");
        });

        modelBuilder.Entity<MedicalRecord>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PRIMARY");

            entity.ToTable("medical_records");

            entity.HasIndex(e => e.DoctorId, "doctor_id");

            entity.HasIndex(e => e.NurseId, "nurse_id");

            entity.HasIndex(e => e.PatientId, "patient_id");

            entity.Property(e => e.RecordId)
                .HasColumnType("int(11)")
                .HasColumnName("record_id");
            entity.Property(e => e.Diagnosis)
                .HasMaxLength(200)
                .HasColumnName("diagnosis");
            entity.Property(e => e.DoctorId)
                .HasColumnType("int(11)")
                .HasColumnName("doctor_id");
            entity.Property(e => e.NurseId)
                .HasColumnType("int(11)")
                .HasColumnName("nurse_id");
            entity.Property(e => e.PatientId)
                .HasColumnType("int(11)")
                .HasColumnName("patient_id");
            entity.Property(e => e.RecordDate)
                .HasColumnType("datetime")
                .HasColumnName("record_date");
            entity.Property(e => e.Treatment)
                .HasMaxLength(200)
                .HasColumnName("treatment");

            entity.HasOne(d => d.Doctor).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("medical_records_ibfk_2");

            entity.HasOne(d => d.Nurse).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.NurseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("medical_records_ibfk_3");

            entity.HasOne(d => d.Patient).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("medical_records_ibfk_1");
        });

        modelBuilder.Entity<Nurse>(entity =>
        {
            entity.HasKey(e => e.NurseId).HasName("PRIMARY");

            entity.ToTable("nurses");

            entity.Property(e => e.NurseId)
                .HasColumnType("int(11)")
                .HasColumnName("nurse_id");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .HasColumnName("phone_number");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PRIMARY");

            entity.ToTable("patients");

            entity.Property(e => e.PatientId)
                .HasColumnType("int(11)")
                .HasColumnName("patient_id");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .HasColumnName("address");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasColumnType("enum('M','F','O')")
                .HasColumnName("gender");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(15)
                .HasColumnName("phone_number");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("PRIMARY");

            entity.ToTable("permissions");

            entity.Property(e => e.PermissionId)
                .HasColumnType("int(11)")
                .HasColumnName("permission_id");
            entity.Property(e => e.PermissionName)
                .HasMaxLength(50)
                .HasColumnName("permission_name");
        });

        modelBuilder.Entity<Prescription>(entity =>
        {
            entity.HasKey(e => e.PrescriptionId).HasName("PRIMARY");

            entity.ToTable("prescriptions");

            entity.HasIndex(e => e.DrugId, "drug_id");

            entity.HasIndex(e => e.RecordId, "record_id");

            entity.Property(e => e.PrescriptionId)
                .HasColumnType("int(11)")
                .HasColumnName("prescription_id");
            entity.Property(e => e.DrugId)
                .HasColumnType("int(11)")
                .HasColumnName("drug_id");
            entity.Property(e => e.Instructions)
                .HasMaxLength(255)
                .HasColumnName("instructions");
            entity.Property(e => e.Quantity)
                .HasColumnType("int(11)")
                .HasColumnName("quantity");
            entity.Property(e => e.RecordId)
                .HasColumnType("int(11)")
                .HasColumnName("record_id");

            entity.HasOne(d => d.Drug).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.DrugId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prescriptions_ibfk_1");

            entity.HasOne(d => d.Record).WithMany(p => p.Prescriptions)
                .HasForeignKey(d => d.RecordId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("prescriptions_ibfk_2");
        });

        modelBuilder.Entity<SoldDrug>(entity =>
        {
            entity.HasKey(e => e.SaleId).HasName("PRIMARY");

            entity.ToTable("sold_drugs");

            entity.HasIndex(e => e.DrugId, "drug_id");

            entity.HasIndex(e => e.PatientId, "patient_id");

            entity.Property(e => e.SaleId)
                .HasColumnType("int(11)")
                .HasColumnName("sale_id");
            entity.Property(e => e.DrugId)
                .HasColumnType("int(11)")
                .HasColumnName("drug_id");
            entity.Property(e => e.PatientId)
                .HasColumnType("int(11)")
                .HasColumnName("patient_id");
            entity.Property(e => e.Quantity)
                .HasColumnType("int(11)")
                .HasColumnName("quantity");
            entity.Property(e => e.SaleDate)
                .HasColumnType("datetime")
                .HasColumnName("sale_date");
            entity.Property(e => e.TotalPrice)
                .HasPrecision(10, 2)
                .HasColumnName("total_price");

            entity.HasOne(d => d.Drug).WithMany(p => p.SoldDrugs)
                .HasForeignKey(d => d.DrugId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sold_drugs_ibfk_1");

            entity.HasOne(d => d.Patient).WithMany(p => p.SoldDrugs)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sold_drugs_ibfk_2");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("users");

            entity.HasIndex(e => e.RoleId, "role_id");

            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
            entity.Property(e => e.RoleId)
                .HasColumnType("int(11)")
                .HasColumnName("role_id");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_ibfk_1");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.ToTable("user_roles");

            entity.Property(e => e.RoleId)
                .HasColumnType("int(11)")
                .HasColumnName("role_id");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");

            entity.HasMany(d => d.Permissions).WithMany(p => p.Roles)
                .UsingEntity<Dictionary<string, object>>(
                    "RolePermission",
                    r => r.HasOne<Permission>().WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("role_permissions_ibfk_2"),
                    l => l.HasOne<UserRole>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("role_permissions_ibfk_1"),
                    j =>
                    {
                        j.HasKey("RoleId", "PermissionId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("role_permissions");
                        j.HasIndex(new[] { "PermissionId" }, "permission_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
