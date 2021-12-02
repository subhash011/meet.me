﻿/// <author>Siddharth Sha</author>
/// <created>15/11/2021</created>
/// <summary>
///		This file contains the utility
///		functions for testing purpose
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using Content;
using Dashboard;
using Dashboard.Server.Telemetry;

namespace Testing.Dashboard
{
    /// <summary>
    ///     Utility class for testing
    /// </summary>
    public static class Utils
    {
#pragma warning disable SecurityIntelliSenseCS // MS Security rules violation
        private static readonly Random random = new();
#pragma warning restore SecurityIntelliSenseCS // MS Security rules violation
        /// <summary>
        ///     Generates valid IPs and Ports
        /// </summary>
        /// <returns> A valid IP and port {IP:port}</returns>
        public static string GenerateValidIPAndPort()
        {
            return
                $"{random.Next(1, 255)}.{random.Next(0, 255)}.{random.Next(0, 255)}.{random.Next(0, 255)}:{random.Next(0, 65535)}";
        }

        /// <summary>
        ///     Generates meeting credentials given Ip and port
        /// </summary>
        /// <param name="ipAddressAndPort"> IP and port in the form {IP:port}</param>
        /// <returns>returns corresponding meeting credentials</returns>
        public static MeetingCredentials GenerateMeetingCreds(string ipAddressAndPort)
        {
            var colonIndex = ipAddressAndPort.IndexOf(':');
            if (colonIndex == -1)
                return null;
            var ipAddress = ipAddressAndPort.Substring(0, colonIndex);
            var port = int.Parse(ipAddressAndPort.Substring(colonIndex + 1));
            var _meetCreds = new MeetingCredentials(ipAddress, port);
            return _meetCreds;
        }

        private static string GetRandomString(int length)
        {
            var b = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_.";


            var randomString = "";

            for (var i = 0; i < length; i++)
            {
                var a = random.Next(54);
                randomString += b.ElementAt(a);
            }

            return randomString;
        }

        /// <summary>
        ///     Generates random user data
        /// </summary>
        /// <returns>returns list of users for testing</returns>
        public static List<UserData> GenerateUserData(int size = 10)
        {
            var users = new List<UserData>();
            for (var i = 0; i < size; i++) users.Add(new UserData(GetRandomString(random.Next(10)), i + 1));
            return users;
        }

        /// <summary>
        ///     Creates a sample session object
        /// </summary>
        /// <param name="size"> size of users to be inserted in session object </param>
        /// <returns>sample session object</returns>
        public static SessionData GenerateSampleSessionData(int size)
        {
            SessionData sData = new();
            for (var i = 0; i < size; i++) sData.AddUser(new UserData(GetRandomString(random.Next(10)), i));
            return sData;
        }

        /// <summary>
        ///     Generates a sample chat context
        /// </summary>
        /// <param name="size"> size of chats in chat context </param>
        /// a
        /// <returns>Sample Chat Context List</returns>
        public static List<ChatContext> GetSampleChatContext(int size = 50)
        {
            List<ChatContext> chats = new();
            for (var i = 0; i < size; i++)
            {
                ChatContext c = new();
                List<ReceiveMessageData> receiveMessageDatas = new();
                for (var j = 0; j < 5; j++)
                {
                    ReceiveMessageData data = new();
                    data.Message = "Hi from " + (i + j);
                    if (i % 5 == 0)
                        data.Message += ".This is special";
                    data.Type = MessageType.Chat;
                    data.Starred = i % 5 == 0;
                    receiveMessageDatas.Add(data);
                }

                c.MsgList = receiveMessageDatas;
                chats.Add(c);
            }

            return chats;
        }


        public static List<ChatContext> GetSampleChatContextForUsers(List<UserData> users, int size = 5)
        {
            List<ChatContext> chats = new();
            for (var i = 0; i < size; i++)
            {
                ChatContext c = new();
                List<ReceiveMessageData> receiveMessageDatas = new();
                for (var j = 0; j < users.Count; j++)
                {
                    ReceiveMessageData data = new();
                    data.Message = "Hi from " + (i + j);
                    if (i % 5 == 0)
                        data.Message += ".This is special";
                    data.Type = MessageType.Chat;
                    data.Starred = i % 5 == 0;
                    data.SenderId = users[i].userID;
                    receiveMessageDatas.Add(data);
                }

                c.MsgList = receiveMessageDatas;
                chats.Add(c);
            }

            return chats;
        }

        public static SessionAnalytics GenerateSessionAnalyticsData()
        {
            var analyticsData = new SessionAnalytics();
            analyticsData.userCountAtAnyTime = new Dictionary<DateTime, int>();
            analyticsData.chatCountForEachUser = new Dictionary<int, int>();
            analyticsData.insincereMembers = new List<int>();
            analyticsData.userCountAtAnyTime.Add(new DateTime(2021, 12, 10), 5);
            analyticsData.userCountAtAnyTime.Add(new DateTime(2021, 12, 11), 5);
            analyticsData.chatCountForEachUser.Add(1, 5);
            analyticsData.chatCountForEachUser.Add(2, 10);
            analyticsData.insincereMembers.Add(1);
            analyticsData.insincereMembers.Add(2);
            return analyticsData;
        }
    }
}