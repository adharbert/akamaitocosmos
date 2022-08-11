using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{

    #region " ****  Sub classes **** "

    public class Ams
    {
        public int CustomerNumber { get; set; }
        public string? CustomerId { get; set; }
        public string? FullNameString { get; set; }
        public DateTime LastSync { get; set; }
    }

    public class Client
    {
        public int Id { get; set; }
        public string? ClientId { get; set; }
        public string? Name { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? FirstLogin { get; set; }
    }

    public class ContactInfo
    {
        public string? Address1 { get; set; }
        public string? Address2 { get; set; }
        public string? City { get; set; }
        public string? StateAbbreviation { get; set; }
        public string? Zip { get; set; }
        public string? ZipPlus4 { get; set; }
        public string? Country { get; set; }
        public string? Company { get; set; }
        public string? Phone { get; set; }
    }

    public class StCode
    {
        public string? Id { get; set; }
        public string? StateCode { get; set; }
    }


    public class DNC
    {
        public string? Source { get; set; }
        public bool? Status { get; set; }
        public string? Name { get; set; }
        public DateTime? Date { get; set; }
    }

    public class StateDNC : DNC
    {
        public List<StCode>? StateCodes { get; set; }
    }


    // Root call
    public class DoNotCall
    {
        public DNC? OtherTPSDNC { get; set; }
        public DNC? FederalDNC { get; set; }
        public DNC? InternalDNC { get; set; }
        public StateDNC? StateDNC { get; set; }
        public DNC? WirelessDNC { get; set; }

    }

    public class Email
    {
        public string? Id { get; set; }
        public string? Address { get; set; }
        public string? Description { get; set; }
        public string? Source { get; set; }
        public DateTime? validated { get; set; }
    }


    public class EmailOptin
    {
        public bool Status { get; set; }
        public bool ExplicitOptOut { get; set; }
        public string? ExplicitOptOutSource { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? ExplicitOptOutDate { get; set; }
    }


    public class KnownLitigator
    {
        public bool? Status { get; set; }
        public DateTime? LastUpdate { get; set; }
    }


    public class Phone
    {
        public string? Raw { get; set; }
        public bool? Litigator { get; set; }
        public bool? Validated { get; set; }
        public string? AmsId { get; set; }
        public string? Source { get; set; }
        public string? Id { get; set; }
        public bool? MatchOwnership { get; set; }
        public string? Type { get; set; }
        public string? Number { get; set; }
        public string? Description { get; set; }
    }

    public class Photo
    {
        public string? Id { get; set; }
        public string? Type { get; set; }
        public string? Value { get; set; }
    }

    public class Primary
    {
        public int? IdRef { get; set; }
        public string? Source { get; set; }
        public string? Number { get; set; }
    }


    public class SmsOptin
    {
        public bool? Status { get; set; } = false;
        public bool? ExplicitOptOut { get; set; } = false;
        public string? ExplicitOptOutSource { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? ExplicitOptOutDate { get; set; }
    }

    public class StatusDetail
    {
        public string? Id { get; set; }
        public string? Status { get; set; }
        public string? StatusCreated { get; set; }
    }


    public class OptIn
    {
        public bool? Status { get; set; } = false;
        public DateTime? Updated { get; set; }
    }

    public class CloudSearch
    {
        public int? SyncAttempts { get; set; }
        public DateTime? SyncUpdated { get; set; }
    }

    public class ManagedBy
    {
        public int? Id { get; set; }
        public string? ClientId { get; set; }
    }

    public class Properties
    {
        public List<ManagedBy>? ManagedBy { get; set; }
    }


    // Root class here
    public class Janrain
    {
        public CloudSearch? CloudSearch { get; set; }
        public Properties? Properties { get; set; }

    }

    #endregion


    public class AkamaiUser : BaseEntity
    {
        public string Uuid { get; set; } = Guid.NewGuid().ToString();
        public string? GivenName { get; set; } // First Name
        public string? MiddleName { get; set; }
        public string? FamilyName { get; set; } // Last Name
        public string CustomETag { get; set; }
        public string? DisplayName { get; set; }
        public string? Prefix { get; set; }
        public string? Suffix { get; set; }
        public string? Gender { get; set; }
        public string? CurrentLocation { get; set; }
        public Ams? Ams { get; set; }
        public Janrain? Janrain { get; set; }
        public DoNotCall? DoNotCall { get; set; }
        public string? MobileNumber { get; set; }
        public List<Email>? Emails { get; set; }
        public string? Email { get; set; }
        public EmailOptin? EmailOptin { get; set; }
        public KnownLitigator? KnownLitigator { get; set; }
        public OptIn? OptIn { get; set; }
        public DateTime? DeactivateAccount { get; set; }
        public List<Phone>? Phones { get; set; }
        public ContactInfo? PrimaryAddress { get; set; }
        public string? PreferredContactMethod { get; set; }
        public string? UserStatus { get; set; }
        public DateTime? ReclaimedDate { get; set; }
        public Primary? PrimaryPhone { get; set; }
        public Primary? PrimarySMS { get; set; }
        //public List<object>? Profiles { get; set; }
        public DateTime? Birthday { get; set; }
        public string? DataHygiene { get; set; }
        public DateTime? LastLogin { get; set; } = DateTime.Now;
        public SmsOptin? SmsOptin { get; set; }
        public List<Photo>? Photos { get; set; }
        //public object? password { get; set; }
        public List<StatusDetail>? Statuses { get; set; }
        public List<Client>? Clients { get; set; }
        public DateTime? EmailVerified { get; set; }
        public string? ReclaimMethod { get; set; }
        public string? AboutMe { get; set; }

        //private string? _id;
        //public string? Id
        //{
        //    get
        //    {
        //        return _id;
        //    }
        //    set
        //    {
        //        _id = value?.ToString();
        //    }
        //}
        public DateTime? MobileNumberVerified { get; set; }
        public string? ExternalId { get; set; }
        public string? HouseholdMemberId { get; set; }
        public string? UserType { get; set; }
        /*
        //public DateTime? Created { get; set; } = DateTime.Now;
        //public DateTime? LastUpdated { get; set; } = DateTime.Now;*/
        
    }



    public class AkamaiResponse
    {
        public List<AkamaiUser> results { get; set; }
        public int result_count { get; set; }
        public string stat { get; set; }

    }

}
