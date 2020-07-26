using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.WebControls;

namespace Result_Management_System
{
    public class InputValidation
    {
        public static bool ValidateUnitCode(string unitcode)
        {
            bool pass = true;

            if (unitcode.Trim().Length < 1)
            {
                pass = false;
            }

            if (!(Regex.IsMatch(unitcode, @"^[a-zA-Z]{3}[0-9]{4}$")))
            {
                pass = false;
            }

            return pass;
        }

        public static bool ValidateUserInput(string input)
        {
            bool pass = true;

            if (input.Trim().Length < 1)
            {
                pass = false;
            }

            return pass;
        }

        public static bool ValidateUnitOutline(FileUpload unitoutline)
        {
            bool pass = true;

            if (!unitoutline.HasFile)
            {
                pass = false;
            }
            else
            {
                string extension = System.IO.Path.GetExtension(unitoutline.PostedFile.FileName).ToLower();

                if (!(Regex.IsMatch(extension, @"(.*?)\.(pdf)$")))
                {
                    pass = false;
                }
            }

            return pass;
        }

        public static bool ValidateStudentPhoto(FileUpload studentphoto)
        {
            bool pass = true;

            if (!studentphoto.HasFile)
            {
                pass = false;
            }
            else
            {
                string extension = System.IO.Path.GetExtension(studentphoto.PostedFile.FileName).ToLower();

                if (!(Regex.IsMatch(extension, @"(.*?)\.(jpg|jpeg|png|gif)$")))
                {
                    pass = false;
                }
            }

            return pass;
        }

        public static bool ValidateAssessmentScore(string assessmentscore)
        {
            bool pass = true;

            if (decimal.TryParse(assessmentscore, out decimal result))
            {
                decimal score = decimal.Parse(assessmentscore);

                if (score < 0 || score > 20)
                {
                    pass = false;
                }
            }
            else
            {
                pass = false;
            }

            return pass;
        }

        public static bool ValidateExamScore(string examscore)
        {
            bool pass = true;

            if (decimal.TryParse(examscore, out decimal result))
            {
                decimal score = decimal.Parse(examscore);

                if (score < 0 || score > 60)
                {
                    pass = false;
                }
            }
            else
            {
                pass = false;
            }

            return pass;
        }
    }
}