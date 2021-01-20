using System.Collections.Generic;
using System.Linq;
using System.Net;
using ProjectSystem;

namespace BlockChainClient.P2P {
    public class CoreNodeList {
        private HashSet<IPEndPoint> list = new HashSet<IPEndPoint>();

        public void Add(IPEndPoint peer) {
            Debugger.Log("Adding peer: " + peer);
            list.Add(peer);
            Debugger.Log("Current Core List: " + list);
        }

        public void Remove(IPEndPoint peer) {
            if (!list.Contains(peer)) {
                return;
            }

            Debugger.Log("Removing peer: " + peer);
            list.Remove(peer);
            Debugger.Log("Current Core List: " + list);
        }

        public void Overwrite(HashSet<IPEndPoint> newList) {
            Debugger.Log("core node list will be going to overwrite");
            list = newList;
            Debugger.Log("Current Core List" + list);
        }

        public HashSet<IPEndPoint> GetList() {
            return list;
        }

        public int GetLength() {
            return list.Count;
        }

        public IPEndPoint GetCNodeInfo() {
            return list.First();
        }

        public bool HasThisPeer(IPEndPoint peer) {
            return list.Contains(peer);
        }
    }
}
