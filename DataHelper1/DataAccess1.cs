using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataHelper1
{
    public class DataAccess1
    {

        static String myConStr = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\Ellaine\Documents\Visual Studio 2013\WebSites\WebSite5\App_Data\dbase.mdf;Integrated Security=True";
        SqlConnection myConn = new SqlConnection(myConStr);

        //membertable
        String email, name, password, address, phone, question, answer, newpassword;  
        decimal initialdeposit;
    
        //loantable
        decimal loantype, month, loanamount, rate, interest, totalloanamount, processfee, deduction, takehomeloan, monthlyarmotization;
        String status;

     
        #region loan variables
        public String Status
        {
            get { return status; }
            set { status = value; }
        }
        public decimal Monthlyarmotization
        {
            get { return monthlyarmotization; }
            set { monthlyarmotization = value; }
        }

        public decimal Takehomeloan
        {
            get { return takehomeloan; }
            set { takehomeloan = value; }
        }

        public decimal Deduction
        {
            get { return deduction; }
            set { deduction = value; }
        }

        public decimal Processfee
        {
            get { return processfee; }
            set { processfee = value; }
        }

        public decimal Totalloanamount
        {
            get { return totalloanamount; }
            set { totalloanamount = value; }
        }

        public decimal Interest
        {
            get { return interest; }
            set { interest = value; }
        }

        public decimal Rate
        {
            get { return rate; }
            set { rate = value; }
        }

        public decimal Loanamount
        {
            get { return loanamount; }
            set { loanamount = value; }
        }

        public decimal Month
        {
            get { return month; }
            set { month = value; }
        }

        

        public decimal Loantype
        {
            get { return loantype; }
            set { loantype = value; }
        }

        #endregion

        #region registration variables

        public Decimal Initialdeposit
        {
            get { return initialdeposit; }
            set { initialdeposit = value; }
        }

        public String Answer
        {
            get { return answer; }
            set { answer = value; }
        }

        public String Question
        {
            get { return question; }
            set { question = value; }
        }

        public String Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public String Address
        {
            get { return address; }
            set { address = value; }
        }

        public String Password
        {
            get { return password; }
            set { password = value; }
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public String Email
        {
            get { return email; }
            set { email = value; }
        }

        public String Newpassword
        {
            get { return newpassword; }
            set { newpassword = value; }
        }

        #endregion

        #region computations
        public decimal CompLoanAmount()
        {
            loanamount = loantype * initialdeposit;
            return loanamount;
        }

        public decimal CompRate()
        {
            rate = 0.0075M * month ;
            return rate;
        }

        public decimal CompInterest()
        {
            interest = CompLoanAmount() * CompRate();
            return interest;
        }

        public decimal CompTotalLoanAmount()
        {
            totalloanamount = CompLoanAmount() + (month * CompRate());
            return totalloanamount;
        }

        public decimal CompProcessFee()
        {
            processfee = 0.02M * CompTotalLoanAmount();
            return processfee;
        }

        public decimal CompDeduction()
        {
            deduction = CompInterest() + CompProcessFee();
            return deduction;
        }

        public decimal CompTakeHome()
        {
            takehomeloan = CompTotalLoanAmount() - CompDeduction();
            return takehomeloan;
        }

        public decimal CompMonthlyAmortization()
        {
            monthlyarmotization = CompTotalLoanAmount() / month;
            return monthlyarmotization;
        }
        #endregion

        public bool RegisterMember()
        {
            bool validData = true;

            myConn.Open();
            if (validData)
            {
                SqlCommand saveCmd = new SqlCommand("registerMember", myConn);
                saveCmd.CommandType = CommandType.StoredProcedure;
                saveCmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Email;
                saveCmd.Parameters.Add("@Name", SqlDbType.NVarChar).Value = Name;
                saveCmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = Password;
                saveCmd.Parameters.Add("@Address", SqlDbType.NVarChar).Value = Address;
                saveCmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = Phone;
                saveCmd.Parameters.Add("@InitialDeposit", SqlDbType.Decimal).Value = Initialdeposit;
                saveCmd.Parameters.Add("@Question", SqlDbType.NVarChar).Value = Question;
                saveCmd.Parameters.Add("@Answer", SqlDbType.NVarChar).Value = Answer;
                saveCmd.ExecuteNonQuery();
                myConn.Close();
                return true;
            } else
            {
                myConn.Close();
                return false;
            }
        }

        public bool CheckMember()
        {
            bool validData = false;

            myConn.Open();
            SqlCommand readCmd = new SqlCommand("checkMember", myConn);
            readCmd.CommandType = CommandType.StoredProcedure;

            readCmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Email;
            readCmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = Password;
            SqlDataReader dr;
            dr = readCmd.ExecuteReader();

            while (dr.Read())
            {
                validData = true;
                email = dr.GetString(1);
                break;
            }

            myConn.Close();
            return validData;
        }

        public bool RegisterAdmin()
        {
            bool validData = true;

            myConn.Open();
            if (validData)
            {
                SqlCommand saveCmd = new SqlCommand("registerAdmin", myConn);
                saveCmd.CommandType = CommandType.StoredProcedure;
                saveCmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Email;
              
                saveCmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = Password;
                
                saveCmd.Parameters.Add("@Question", SqlDbType.NVarChar).Value = Question;
                saveCmd.Parameters.Add("@Answer", SqlDbType.NVarChar).Value = Answer;
                saveCmd.ExecuteNonQuery();
                myConn.Close();
                return true;
            }
            else
            {
                myConn.Close();
                return false;
            }
        }

        public bool CheckAdmin()
        {
            bool validData = false;

            myConn.Open();
            SqlCommand readCmd = new SqlCommand("checkAdmin", myConn);
            readCmd.CommandType = CommandType.StoredProcedure;

            readCmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Email;
            readCmd.Parameters.Add("@Password", SqlDbType.NVarChar).Value = Password;
            SqlDataReader dr;
            dr = readCmd.ExecuteReader();

            while (dr.Read())
            {
                validData = true;
                email = dr.GetString(1);
                break;
            }

            myConn.Close();
            return validData;
        }

        public bool MemberRecovery()
        {
            bool validData = false;

            myConn.Open();
            SqlCommand readcmd = new SqlCommand("memberRecovery", myConn);
            readcmd.CommandType = CommandType.StoredProcedure;
            readcmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Email;
            SqlDataReader dr;
            dr = readcmd.ExecuteReader();

            while (dr.Read())
            {
                validData = true;
                question = dr.GetString(0);
                answer = dr.GetString(1);
                break;
            }

            myConn.Close();
            return validData;
        }

        public bool UpdatePassword()
        {
            myConn.Open();
            SqlCommand readcmd = new SqlCommand("updatePassword", myConn);
            readcmd.CommandType = CommandType.StoredProcedure;
            readcmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Email;
            readcmd.Parameters.Add("@NewPassword", SqlDbType.NVarChar).Value = Newpassword;

            readcmd.ExecuteNonQuery();
            myConn.Close();

            return true;
        }

        public bool AdminRecovery()
        {
            bool validData = false;

            myConn.Open();
            SqlCommand readcmd = new SqlCommand("adminRecovery", myConn);
            readcmd.CommandType = CommandType.StoredProcedure;
            readcmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Email;
            SqlDataReader dr;
            dr = readcmd.ExecuteReader();

            while (dr.Read())
            {
                validData = true;
                question = dr.GetString(0);
                answer = dr.GetString(1);
                break;
            }

            myConn.Close();
            return validData;
        }

        public bool UpdatePasswordAdmin()
        {
            myConn.Open();
            SqlCommand readcmd = new SqlCommand("updatePasswordAdmin", myConn);
            readcmd.CommandType = CommandType.StoredProcedure;
            readcmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Email;
            readcmd.Parameters.Add("@NewPassword", SqlDbType.NVarChar).Value = Newpassword;

            readcmd.ExecuteNonQuery();
            myConn.Close();

            return true;
        }

        public bool GetInitialDeposit()
        {
            bool validData = false;

            myConn.Open();
            SqlCommand readcmd = new SqlCommand("getInitialDeposit", myConn);
            readcmd.CommandType = CommandType.StoredProcedure;
            readcmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Email;
            SqlDataReader dr;
            dr = readcmd.ExecuteReader();

            while (dr.Read())
            {
                validData = true;
                initialdeposit = dr.GetDecimal(0);
                break;
            }

            myConn.Close();
            return validData;
        }

        public bool UpdateDeposit()
        {
            myConn.Open();
            SqlCommand readcmd = new SqlCommand("updateDeposit", myConn);
            readcmd.CommandType = CommandType.StoredProcedure;
            readcmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Email;
            readcmd.Parameters.Add("@NewDeposit", SqlDbType.Decimal).Value = Initialdeposit;
            readcmd.ExecuteNonQuery();
            myConn.Close();

            return true;
        }

        public bool ValidateLoan()
        {
            bool validInfo = false;

            myConn.Open();
            SqlCommand readcmd = new SqlCommand("validateLoan", myConn);
            readcmd.CommandType = CommandType.StoredProcedure;

            readcmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Email;
            readcmd.Parameters.Add("@InitialDeposit", SqlDbType.NVarChar).Value = Initialdeposit;
            SqlDataReader dr;
            dr = readcmd.ExecuteReader();

            while (dr.Read())
            {
                validInfo = true;
                email = dr.GetString(0);
                break;
            }

            myConn.Close();
            return validInfo;
        }

        public bool ComputeEverything()
        {
            bool validData = false;

            myConn.Open();
            SqlCommand readcmd = new SqlCommand("computeEverything", myConn);
            readcmd.CommandType = CommandType.StoredProcedure;
            readcmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Email;
            SqlDataReader dr;
            dr = readcmd.ExecuteReader();

            while (dr.Read())
            {
                validData = true;
                initialdeposit = dr.GetDecimal(0);
                break;
            }

            myConn.Close();
            return validData;
        }

        public bool SaveApplication()
        {
            bool validInfo = true;

            myConn.Open();
            if (validInfo)
            {
                SqlCommand saveCmd = new SqlCommand("saveApplication", myConn);
                saveCmd.CommandType = CommandType.StoredProcedure;

                saveCmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Email;
                saveCmd.Parameters.Add("@InitialDeposit", SqlDbType.Float).Value = Initialdeposit;
                saveCmd.Parameters.Add("@AmountLoan", SqlDbType.Float).Value = Loanamount;
                saveCmd.Parameters.Add("@Rate", SqlDbType.Float).Value = Rate;
                saveCmd.Parameters.Add("@Interest", SqlDbType.Float).Value = Interest;
                saveCmd.Parameters.Add("@TotalAmountLoan", SqlDbType.Float).Value = Totalloanamount;
                saveCmd.Parameters.Add("@ProcessFee", SqlDbType.Float).Value = Processfee;
                saveCmd.Parameters.Add("@Deduction", SqlDbType.Float).Value = Deduction;
                saveCmd.Parameters.Add("@TakeHomeLoan", SqlDbType.Float).Value = Takehomeloan; ;
                saveCmd.Parameters.Add("@MonthlyAmortization", SqlDbType.Float).Value = Monthlyarmotization;

                saveCmd.ExecuteNonQuery();
                myConn.Close();
                return true;
            }

            else
            {
                myConn.Close();
                return false;
            }
        }

        public bool SearchUser()
        {
            bool validUser = false;

            myConn.Open();
            SqlCommand readcmd = new SqlCommand("searchUser", myConn);
            readcmd.CommandType = CommandType.StoredProcedure;

            readcmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Email;
            SqlDataReader dr;
            dr = readcmd.ExecuteReader();
            while (!validUser && dr.Read())
            {
                Email = dr.GetString(0);
                Initialdeposit = Convert.ToDecimal(dr.GetDouble(1));
                Loanamount = Convert.ToDecimal(dr.GetDouble(2));
                Rate = Convert.ToDecimal(dr.GetDouble(3));
                Interest = Convert.ToDecimal(dr.GetDouble(4));
                Totalloanamount = Convert.ToDecimal(dr.GetDouble(5));
                Processfee = Convert.ToDecimal(dr.GetDouble(6));
                Deduction = Convert.ToDecimal(dr.GetDouble(7));
                Takehomeloan = Convert.ToDecimal(dr.GetDouble(8));
                Monthlyarmotization = Convert.ToDecimal(dr.GetDouble(9));
                Status = dr.GetString(10);
                validUser = true;
                break;
            }

            myConn.Close();
            return validUser;
        }

        public DataSet SelectRecord()
        {
            SqlDataAdapter adpt = new SqlDataAdapter("selectRecord", myConn);
            DataSet ds = new DataSet();
            adpt.Fill(ds, "tableData");
            return ds;
        }


        public bool UpdateLoanStatus()
        {
            myConn.Open();
            SqlCommand readcmd = new SqlCommand("updateLoanStatus", myConn);
            readcmd.CommandType = CommandType.StoredProcedure;
            readcmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Email;
            readcmd.Parameters.Add("@LoanStatus", SqlDbType.NVarChar).Value = Status;

            readcmd.ExecuteNonQuery();
            myConn.Close();

            return true;
        }
    }
}
