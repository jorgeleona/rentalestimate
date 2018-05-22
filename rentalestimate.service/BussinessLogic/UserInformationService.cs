using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using rentalestimate.dataaccess.Interfaces;
using rentalestimate.model.Users;
using rentalestimate.model.Zillow;
using rentalestimate.service.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace rentalestimate.service.BussinessLogic
{
	public class UserInformationService : IUserInformationService
    {
		private IUserInformationRepository _userInformationRepository;
		private IZillowService _zillowService;

		private int ANNUAL_RENT_PERCENTAGE;
		private int MONTHLY_RANGE_PERCENTAGE;
		private string SENDGRID_APIKEY;
		private string SOURCE_EMAIL;

		public UserInformationService(
			IUserInformationRepository userInformationRepository,
			IZillowService zillowService)
        {
			_userInformationRepository = userInformationRepository;
			_zillowService = zillowService;
			LoadConfiguration();
        }

       
		public UserInformation AddUser(UserInformation newUser)
        {
			SearchResult result = _zillowService.GetSearchResults(newUser);
			newUser = CalculateEstimatedRent(result, newUser);
			newUser = _userInformationRepository.AddUser(newUser);
			SendConfirmationEmail(newUser);
			return newUser;
        }

		public List<UserInformation> GetUsers()
        {

			return _userInformationRepository.GetUsers();
        }

		private UserInformation CalculateEstimatedRent(SearchResult zillowSearch, UserInformation userInfo){

			if(zillowSearch.rentZestimateValuationLow > 0 && 
			   zillowSearch.renrZestimateValuationHigh > 0){

				userInfo.MonthlyRangeLow = zillowSearch.rentZestimateValuationLow;
				userInfo.MonthlyRangeHigh = zillowSearch.renrZestimateValuationHigh;

			}else if(zillowSearch.zestimateAmount > 0){

				decimal baseAmount = ((decimal)ANNUAL_RENT_PERCENTAGE / 100) * zillowSearch.zestimateAmount;
				decimal monthlyRange = ((decimal)MONTHLY_RANGE_PERCENTAGE / 100) * baseAmount;
				userInfo.MonthlyRangeLow = (baseAmount - monthlyRange)/12;
				userInfo.MonthlyRangeHigh = (baseAmount + monthlyRange)/12;

			}
			else{
				userInfo.MonthlyRangeLow = 0;
				userInfo.MonthlyRangeHigh = 0;
			}

			return userInfo;
		}

		private void LoadConfiguration()
		{
			var config = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json")
				.Build();

			ANNUAL_RENT_PERCENTAGE = !string.IsNullOrEmpty(config["annual_rent_percetange"]) ?
											Int32.Parse(config["annual_rent_percetange"]) : 0;

			MONTHLY_RANGE_PERCENTAGE = !string.IsNullOrEmpty(config["monthly_range_percentage"])? 
			                       Int32.Parse(config["monthly_range_percentage"]) : 0;

			SENDGRID_APIKEY = !string.IsNullOrEmpty(config["sendgridapikey"]) ?
			                                  config["sendgridapikey"] : string.Empty;  

			SOURCE_EMAIL = !string.IsNullOrEmpty(config["sourceemail"]) ?
			                      config["sourceemail"] : string.Empty;  

		}
        
		private void SendConfirmationEmail(UserInformation userInfo){
            
			SendGridClient client = new SendGridClient(SENDGRID_APIKEY);
			EmailAddress from = new EmailAddress(SOURCE_EMAIL, "Rental Estimate");
			List<EmailAddress> tos = new List<EmailAddress>
			{
				new EmailAddress(userInfo.EMail, $"{userInfo.FirstName} {userInfo.LastName}")
			};
            string subject = "Rental Estimate - Subscription confirmation";
			string htmlContent = GetEMailBody(userInfo);            
			SendGridMessage msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, "", htmlContent, false);
            client.SendEmailAsync(msg);
		}

		private string GetEMailBody(UserInformation userInformation) {

		    string emailBody = string.Empty;
		    string emailTemplate = string.Empty;

		    string name = $"{userInformation.FirstName} {userInformation.LastName}";
			string fullAddress = $"{userInformation.Address},{userInformation.City},{userInformation.StateCode}";


			//string basedir = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
			//string templateLocation = Path.Combine( basedir,"rentalestimate.service", "subscriptionconfirmationtemplate.html");            

			emailTemplate = GetTemplate();//File.ReadAllText(templateLocation);
			emailBody = !string.IsNullOrEmpty(emailTemplate) ? 
			                   String.Format(emailTemplate, name, userInformation.PhoneNumber, 
			                                 userInformation.EMail, fullAddress, 
			                                 userInformation.MonthlyRangeLow.ToString("C2"), 
			                                 userInformation.MonthlyRangeHigh.ToString("C2")) : "";
		    return emailBody;
		}

		private string GetTemplate(){

			return 
				"<!DOCTYPE html>"+
				"<html>" +
				"<head>" +
				"<meta charset = \"utf-8\"/>" +  
				"<title></title>" +
				"</head>" +
				"<body>" +
				"<table>" +
				"<tr><td> Subscriber </td><td>{0}</td></tr>" +
				"<tr><td> Phone number </td><td>{1}</td></tr>" +
				"<tr><td> EMail </td><td>{2}</td></tr>" +
				"<tr><td> Address </td><td>{3}</td></tr>" +
				"<tr><td> Estimated monthly rental</td><td> From {4} to {5}</td></tr>" +
				"</table>"+
				"</body></html>";
		}
    }
}
