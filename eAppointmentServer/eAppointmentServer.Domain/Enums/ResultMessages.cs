namespace eAppointmentServer.Domain.Enums;

public static class ResultMessages
{
  public const string RECORD_ADDED = "Record Added";
  public const string RECORD_DELETED = "Record Deleted";
  public const string RECORD_UPDATED = "Record Updated";
  public const string NO_RECORD = "No Record";
  public const string INVALID_DATE = "Invalid Date";
  public const string SAME_DATE = "You Have Already an Appointment at This Time";
  public const string WRONG_PASSWORD = "Password is Wrong";
  public const string SYNC_ROLES = "Roles are Syncronized";
  public const string NO_USER = "User Not Found";
  public const string SAME_IDENTITY = "Identity Number Already In Use";
  public const string SAME_EMAIL = "Email already exists";
  public const string SAME_USER = "UserName already exists";
}