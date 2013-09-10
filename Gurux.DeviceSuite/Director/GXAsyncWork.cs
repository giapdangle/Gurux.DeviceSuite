//
// --------------------------------------------------------------------------
//  Gurux Ltd
// 
//
//
// Filename:        $HeadURL$
//
// Version:         $Revision$,
//                  $Date$
//                  $Author$
//
// Copyright (c) Gurux Ltd
//
//---------------------------------------------------------------------------
//
//  DESCRIPTION
//
// This file is a part of Gurux Device Framework.
//
// Gurux Device Framework is Open Source software; you can redistribute it
// and/or modify it under the terms of the GNU General Public License 
// as published by the Free Software Foundation; version 2 of the License.
// Gurux Device Framework is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of 
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. 
// See the GNU General Public License for more details.
//
// This code is licensed under the GNU General Public License v2. 
// Full text may be retrieved at http://www.gnu.org/licenses/gpl-2.0.txt
//---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Gurux.Common;

namespace Gurux.DeviceSuite.Director
{
    public enum AsyncState
    {
        Start,
        Finish,
        Cancel
    }

    public delegate void AsyncTransaction(System.Windows.Forms.Control sender, object[] parameters);
    public delegate void AsyncStateChangeEventHandler(System.Windows.Forms.Control sender, AsyncState state, string text);
    
    public delegate void ErrorEventHandler(System.Windows.Forms.Control sender, Exception ex);

    internal class GXAsyncWork
    {
        string Text;
        System.Windows.Forms.Control Sender;
        AsyncTransaction Command;
        object[] Parameters;
        Thread Thread;
        AsyncStateChangeEventHandler OnAsyncStateChangeEventHandler;

        public GXAsyncWork(System.Windows.Forms.Control sender, AsyncStateChangeEventHandler e, AsyncTransaction command, string text, object[] parameters)
        {
            Text = text;
            OnAsyncStateChangeEventHandler = e;
            Sender = sender;
            Command = command;
            Parameters = parameters;
        }

        void OnError(System.Windows.Forms.Control sender, Exception ex)
        {
            System.Windows.Forms.MessageBox.Show(Sender, ex.Message);
        }

        void Run()
        {
            try
            {
                Command(Sender, Parameters);
                if (Sender.InvokeRequired)
                {
                    Sender.BeginInvoke(OnAsyncStateChangeEventHandler, Sender, AsyncState.Finish, null);
                }
                else
                {
                    OnAsyncStateChangeEventHandler(Sender, AsyncState.Finish, null);
                }
            }
            catch (ThreadAbortException)//User has abort the thread.
            {
                return;
            }
            catch (Exception ex)
            {
                if (Sender.InvokeRequired)
                {
                    Sender.Invoke(new ErrorEventHandler(OnError), Sender, ex);
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show(Sender, ex.Message);
                }
            }
        }

        public void Start()
        {
            OnAsyncStateChangeEventHandler(Sender, AsyncState.Start, Text);
            Thread = new Thread(new ThreadStart(Run));
            Thread.IsBackground = true;
            Thread.Start();
        }

        public void Cancel()
        {
            Thread.Abort();
            OnAsyncStateChangeEventHandler(Sender, AsyncState.Cancel, null);
        }
    }
}
