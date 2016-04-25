using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class InputEmailer : MonoBehaviour {

//	private string input;
	private string body;
	private string subject;
	public string playerName;
	public string playerEmail;
	public GameObject updateText;
//	public int inputCount = 1;
	public List<String> commentList = new List<String> ();
	public List<String> inputList = new List<String> ();
	public List<String> emotionList = new List<String> ();
	public List<String> bodyList = new List <String> ();
//	public List<Image> 

	void Start () {

	}

	void Update () {

	}

	public void Mail ()
	{
		WriteEmail ();
		MailMessage mail = new MailMessage();

		mail.From = new MailAddress("harassmentInGames@gmail.com");
		mail.To.Add ("harassmentInGames@gmail.com");
		mail.To.Add (playerEmail);
		mail.IsBodyHtml = true;
		mail.Subject = subject;
		mail.Body = "<!DOCTYPE html>" + 
					"<html>" + 
						"<body>" + 
							"<p>" + body + "</p>" +
						"</body>" + 
					"</html>";

		SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
		smtpServer.Port = 587;
		smtpServer.Credentials = new System.Net.NetworkCredential("harassmentInGames@gmail.com", "harassme23") as ICredentialsByHost;
		smtpServer.EnableSsl = true;
		ServicePointManager.ServerCertificateValidationCallback = 
			delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) 
		{ return true; };

		try {
			smtpServer.Send(mail);
		}
		catch (SmtpFailedRecipientsException ex)
		{
			for (int i = 0; i < ex.InnerExceptions.Length; i++)
			{
				SmtpStatusCode status = ex.InnerExceptions[i].StatusCode;
				if (status == SmtpStatusCode.MailboxBusy ||
					status == SmtpStatusCode.MailboxUnavailable)
				{
					Debug.Log("Delivery failed - retrying in 5 seconds.");
//					System.Threading.Thread.Sleep(5000);
					smtpServer.Send(mail);
//					client.Send(message);
				}
				else
				{
					Console.WriteLine("Failed to deliver message to {0}", 
					ex.InnerExceptions[i].FailedRecipient);
					mail.To.Clear ();
					mail.To.Add ("harassmentingames@gmail.com");
					Debug.Log("Delivery failed - retrying without player email in 1 seconds.");
//					System.Threading.Thread.Sleep(1000);
					smtpServer.Send(mail);

				}
			}
		}
		Debug.Log("success");
	}
	public void WriteEmail () {
		subject = playerName;
		for (int i = 0; i < commentList.Count; i++) {
//			commentList.RemoveAt [i];
			bodyList.Add ("<div id=\"comment\"><h3>In Response To: </h3><p> \"" + commentList[i] + "\" </p></div>");
			bodyList.Add ("<div id=\"response\"><h4>Player said \"" + inputList [i] + "\" and chose the " + emotionList [i] + " emotion.</h4></div>");
		}
//		comments = string.Join (" </h3> <h3> ", commentList.ToArray ());
		body = string.Join (" \n ", bodyList.ToArray ());
		Debug.Log (body);
	}

	public void PlayerInfoUpdate () {
		playerEmail = updateText.GetComponent<InputField> ().text;
		Mail ();
	}

//	bool IsValidEmail(string email)
//	{
//		try {
//			var addr = new System.Net.Mail.MailAddress(email);
//			return true;
//		}
//		catch {
//			return false;
//		}
//	}

}