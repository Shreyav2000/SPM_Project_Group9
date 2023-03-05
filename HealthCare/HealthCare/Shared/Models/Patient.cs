using System;
using System.Collections.Generic;

namespace HealthCare.Shared.Models;

public partial class Patient
{
    public int PatientId { get; set; }

    public string? PatientNo { get; set; }

    public string? AlternateId { get; set; }

    public string Fname { get; set; } = null!;

    public string Lname { get; set; } = null!;

    public string? Title { get; set; }

    public string Sex { get; set; } = null!;

    public DateTime? Dob { get; set; }

    public string? BloodGroup { get; set; }

    public string? Nationality { get; set; }

    public byte[]? Picture { get; set; }

    public string? Occupation { get; set; }

    public string? Consultingroom { get; set; }

    public string? City { get; set; }

    public string? Street { get; set; }

    public string? Province { get; set; }

    public string? PostalAddress { get; set; }

    public string? Telephone { get; set; }

    public string? Telephone2 { get; set; }

    public string? Email { get; set; }

    public DateTime? DateRegistered { get; set; }

    public string? MaritalStatus { get; set; }

    public short? Country { get; set; }

    public string? Region { get; set; }

    public int? Provid { get; set; }

    public string? Nhis { get; set; }

    public string? CardSerial { get; set; }

    public string? NoKdetails { get; set; }

    public string? LegacyPatientId { get; set; }

    public string? LegacyPatientNo { get; set; }

    public bool? IsOnlineVerified { get; set; }
}
