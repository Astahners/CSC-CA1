using System;
using System.Reflection;

namespace CSC_Assignment1_Task4_TalentRestfulWebService.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}