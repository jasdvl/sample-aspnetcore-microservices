using Microsoft.AspNetCore.Components.Forms;

namespace HomeAnalytica.Web.Components.Validation;

/// <summary>
/// A custom implementation of <see cref="FieldCssClassProvider"/> that disables 
/// the application of validation-related CSS classes.
/// </summary>
public class NoValidationStyleFieldCssProvider : FieldCssClassProvider
{
    /// <summary>
    /// Overrides the method to always return an empty string, effectively 
    /// disabling any CSS classes for validation success or error states.
    /// </summary>
    /// <param name="editContext">The <see cref="EditContext"/> representing the state of the form.</param>
    /// <param name="fieldIdentifier">The <see cref="FieldIdentifier"/> for which the CSS class is requested.</param>
    /// <returns>An empty string to avoid applying any validation-related CSS classes.</returns>
    public override string GetFieldCssClass(EditContext editContext, in FieldIdentifier fieldIdentifier)
    {
        return string.Empty;
    }
}
