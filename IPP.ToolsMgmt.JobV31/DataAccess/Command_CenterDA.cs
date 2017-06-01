using System;
using System.Collections.Generic;
using System.Linq;
using Newegg.Oversea.Framework.Entity;
using Newegg.Oversea.Framework.DataAccess;

namespace IPP.ToolsMgmt.JobV31.DataAccess
{
    public static class Command_CenterDA
    {
        public static int UpdateProductB2BSettlementInfo(int ComanyID, string ProductID, decimal SettlementPrice)
        {
            CustomDataCommand command = DataCommandManager.CreateCustomDataCommandFromConfig("UpdateProductB2BSettlementInfo");
            command.AddInputParameter("@ComanyID", System.Data.DbType.Int32);
            command.AddInputParameter("@ProductID", System.Data.DbType.String);
            command.AddInputParameter("@SettlementPrice", System.Data.DbType.Decimal);
            command.SetParameterValue("@ComanyID", ComanyID);
            command.SetParameterValue("@ProductID", ProductID);
            command.SetParameterValue("@SettlementPrice", SettlementPrice);
            return command.ExecuteNonQuery();
        }

        internal static int InsertCategory(string ItemName, int level)
        {
            DataCommand command = DataCommandManager.GetDataCommand("InsertCategory");
            command.SetParameterValue("@ItemName", ItemName);
            command.SetParameterValue("@ItemLevel", level);

            return command.ExecuteScalar<int>();

        }

        internal static int InsertItem(string itemName, int level, int parentId)
        {
            DataCommand command = DataCommandManager.GetDataCommand("InsertItem");
            command.SetParameterValue("@ItemName", itemName.Trim());
            command.SetParameterValue("@ItemLevel", level);
            command.SetParameterValue("@ParentSysNo", parentId);

            object obj = command.ExecuteScalar();

            return Convert.ToInt32(obj);
        }

        internal static void SendMail(string to, string cc, string bcc,string emailSubject, string emailBody)
        {
            DataCommand command = DataCommandManager.GetDataCommand("SendMail");
            command.SetParameterValue("@MailAddress",to);
            command.SetParameterValue("@CCMailAddress",cc);
            command.SetParameterValue("@BCMailAddress",bcc);
            command.SetParameterValue("@MailSubject", emailSubject);
            command.SetParameterValue("@MailBody",emailBody);
            command.ExecuteNonQuery();
        }
    }
}
