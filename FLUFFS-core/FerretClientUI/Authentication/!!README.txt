###########
IMPORTANT
###########

The login windows do not follow MVVM at all.  This is principally because
the PasswordBox does not have a bindable dependency property.  In order
to manage the security of holding password etc., the windows will handle
all of the logic in code behind and pass the provided plain text password
to a static method for authentication immediately.

Chris Wilson - 10/10/15