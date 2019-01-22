using System;
using System.Dynamic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EchoApp.Library.Helper;
using EchoApp.Model.Signaling;
using EchoApp.Services;
using Services.Library.Helper;
using Services.Model.Signaling;
using Microsoft.AspNetCore.Http;

namespace Services
{
    public class ClsSignalingHandler
    {
        public void SignalingHandler(WebSocket webSocket, byte[] Incbuffer,
            WebSocketReceiveResult result)
        {
            var buffer = Incbuffer.Where(x => x != 0).ToArray();
            var strCommand = Encoding.ASCII.GetString(buffer).Replace(@"\0", string.Empty);
            var objBuffer = new Tuple<byte[], int>(null, 0);

            try
            {
                dynamic objSocket = new ExpandoObject();
                objSocket.connection = webSocket;
                objSocket.name = string.Empty;
                objSocket.otherName = string.Empty;

                BaseObject incType = null;
                CandidateObject CandidateType = null;

                
                try
                {
                    //Todo : You Can Do This Part Better That This
                    //Fix Commands With Leave Handeling v1
                    strCommand = StringHelper.FixCommand(strCommand);
                    incType = SerializationHelper.Deserialize<BaseObject>(strCommand);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
                if (incType.type == ClsEnumeration.EnumCommandType.login.ToString())
                {
                    var loginObj = new LoginResult() { type = "login", success = "true" };
                    var incData = SerializationHelper.Deserialize<LoginObject>(strCommand);
                    try
                    {
                        
                        if (ClsStatics.UsersStatic.ContainsKey(incData.name))
                        {
                            ClsStatics.UsersStatic.Remove(incData.name);
                            objSocket.name = incData.name;
                            ClsStatics.UsersStatic.Add(incData.name, objSocket);
                            
                            objBuffer = ObjectHelper.GetObjectBuffer(loginObj);
                            objSocket.connection.SendAsync(
                                new ArraySegment<byte>(objBuffer.Item1, 0, objBuffer.Item2), result.MessageType,
                                result.EndOfMessage, CancellationToken.None);

                            Console.WriteLine($"Send Success Login For User {incData.name}");
                        }
                        else
                        {
                            loginObj.success = "false";
                            objSocket.name = incData.name;
                            ClsStatics.UsersStatic.Add(incData.name, objSocket);
                            objBuffer = ObjectHelper.GetObjectBuffer(loginObj);
                            objSocket.connection.SendAsync(
                                new ArraySegment<byte>(objBuffer.Item1, 0, objBuffer.Item2), result.MessageType,
                                result.EndOfMessage, CancellationToken.None);
                            Console.WriteLine($"Send Success Login For User {incData.name}");
                        }
                    }
                    catch (Exception e)
                    {
                        objBuffer = ObjectHelper.GetObjectBuffer(loginObj);
                        objSocket.connection.SendAsync(
                            new ArraySegment<byte>(objBuffer.Item1, 0, objBuffer.Item2), result.MessageType,
                            result.EndOfMessage, CancellationToken.None);
                        Console.WriteLine($"Send Failed Login For User {incData.name}");
                    }

                }
                if (incType.type == ClsEnumeration.EnumCommandType.offer.ToString())
                {

                    var incData = SerializationHelper.Deserialize<OfferObject>(strCommand);
                    //for ex. UserA wants to call UserB 
                    //if UserB exists then send him offer details 
                    dynamic Dyncon = ClsStatics.UsersStatic[incData.name];
                    if (Dyncon != null)
                    {
                        var offResObj = new OfferResult()
                        { type = "offer", offer = incData.offer, name = incData.Owner };
                        Dyncon.otherName = incData.name;
                        objBuffer = ObjectHelper.GetObjectBuffer(offResObj);
                        Dyncon.connection.SendAsync(new ArraySegment<byte>(objBuffer.Item1, 0, objBuffer.Item2),
                            result.MessageType, result.EndOfMessage, CancellationToken.None);
                        Console.WriteLine($"Sending offer from : {incData.Owner} to: {incData.name}");
                    }

                }
                if (incType.type == ClsEnumeration.EnumCommandType.candidate.ToString())
                {
                    lock (ClsStatics.MessageLockCandidate)
                    {
                        var incData = SerializationHelper.Deserialize<CandidateObject>(strCommand);
                        dynamic Dyncon = ClsStatics.UsersStatic[incData.name];
                        if (Dyncon != null)
                        {
                            var candidateResObj = new CandidateResult()
                            { type = "candidate", candidate = incData.candidate };
                            objBuffer = ObjectHelper.GetObjectBuffer(candidateResObj);
                            Dyncon.connection.SendAsync(new ArraySegment<byte>(objBuffer.Item1, 0, objBuffer.Item2),
                                result.MessageType, result.EndOfMessage, CancellationToken.None);
                            Console.WriteLine("Sending candidate to:", incData.name);
                        }
                    }
                }
                if (incType.type == ClsEnumeration.EnumCommandType.answer.ToString())
                {

                    var incData = SerializationHelper.Deserialize<AnswerObject>(strCommand);
                    dynamic Dyncon = ClsStatics.UsersStatic[incData.name];
                    Console.WriteLine($"Sending answer to: {incData.name}");
                    //for ex. UserB answers UserA 
                    if (Dyncon != null)
                    {
                        var answerObj = new AnswerObject() { type = "answer", answer = incData.answer };
                        objBuffer = ObjectHelper.GetObjectBuffer(answerObj);
                        Dyncon.connection.SendAsync(
                            new ArraySegment<byte>(objBuffer.Item1, 0, objBuffer.Item2), result.MessageType,
                            result.EndOfMessage, CancellationToken.None);
                    }

                }
                else if (incType.type == ClsEnumeration.EnumCommandType.leave.ToString())
                {
                    lock (ClsStatics.MessageLockCandidate)
                    {
                        var incData = SerializationHelper.Deserialize<LoginObject>(strCommand);
                        dynamic Dyncon = ClsStatics.UsersStatic[incData.name];
                        Dyncon.otherName = null;
                        Console.WriteLine(string.Format("Sending answer to: {0}", incData.name));
                        //for ex. UserB answers UserA 

                        if (Dyncon != null)
                        {                            
                            var leaveObj = new BaseObject() { type = "leave" };
                            objBuffer = ObjectHelper.GetObjectBuffer(leaveObj);
                            Dyncon.connection.SendAsync(
                                new ArraySegment<byte>(objBuffer.Item1, 0, objBuffer.Item2), result.MessageType,
                                result.EndOfMessage, CancellationToken.None);
                            ClsStatics.UsersStatic.Remove(incData.name);
                        }       
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            //await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

        }
    }

    public class ClsSignalingServer
    {
        public async Task Signaling(HttpContext context, WebSocket webSocket)
        {

            var incbuffer = new byte[1024 * 4];
            WebSocketReceiveResult result = webSocket.ReceiveAsync(new ArraySegment<byte>(incbuffer), CancellationToken.None).Result;
            while (!result.CloseStatus.HasValue)
            {

                Monitor.Enter(ClsStatics.MessageLockObject);

                var buffer = incbuffer.Where(x => x != 0).ToArray();
                var strCommand = Encoding.ASCII.GetString(buffer).Replace(@"\0", string.Empty);

                try
                {
                    new ClsSignalingHandler().SignalingHandler(webSocket, incbuffer, result);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                Monitor.Exit(ClsStatics.MessageLockObject);
                result = webSocket.ReceiveAsync(new ArraySegment<byte>(incbuffer), CancellationToken.None).Result;
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);

        }
    }
}