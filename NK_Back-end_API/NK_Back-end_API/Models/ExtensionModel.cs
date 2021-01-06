using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace NK_Back_end_API.Models
{
    public static class ExtensionModel
    {
        // ปรับแต่งค่า Error ของModelsState ใหม่
        public static string GetErrorModelsState(this ModelStateDictionary modelsState)
        {
            var modelValue = modelsState.Values
                .Select(Values => Values.Errors)
                .Where(value=>value.Count()>0)
                .FirstOrDefault();

            if (modelValue == null) return null;
            return modelValue[0].ErrorMessage;
        }

        // ปรับแต่งค่า Error Exception แสดงค่า inner Exception ในสุด
        public static Exception GetErrorException(this Exception exception) {
            if (exception.InnerException != null) 
                return exception.InnerException.GetErrorException();
            return exception;
        }

    }
}