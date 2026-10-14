namespace AlertSync.Web.Layout;

public static class LayoutNotices
{
    public const string ValidForm =
        "TCIE-2: this form is valid and the screen is connected. Nothing was written to the database. Saving is part of the final submission.";

    public const string Confirmed =
        "TCIE-2: the confirmation step is connected. The database was not changed. Delete and update will be saved in the final submission.";

    public const string PasswordReset =
        "TCIE-2: no reset email was sent and no reset token was created. Email delivery is part of the final submission.";
}
