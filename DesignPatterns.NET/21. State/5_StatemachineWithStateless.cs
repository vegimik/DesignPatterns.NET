using Stateless;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.NET._21._State
{
    //  This is prety much related to some above exercise done from us: like example
    //  1.  _2_HandmadeStateMachine
    //  2.  _4_SwitchExpression
    internal class _5_StatemachineWithStateless
    {
        public static bool ParentsNotWatching
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public enum Health
        {
            NonReproductive,
            Pregnant,
            Reproductive
        }

        public enum Activity
        {
            GiveBirth,
            ReachPuberty,
            HaveAbortion,
            HaveUnprotectedSex,
            Historectomy
        }

        public static void Drive()
        {
            var stateMachien = new StateMachine<Health, Activity>(Health.NonReproductive);
            stateMachien
                .Configure(Health.NonReproductive)
                .Permit(Activity.ReachPuberty, Health.Reproductive);


            stateMachien = new StateMachine<Health, Activity>(Health.NonReproductive);
            stateMachien
                .Configure(Health.NonReproductive)
                .Permit(Activity.ReachPuberty, Health.Reproductive);
            stateMachien
                .Configure(Health.Reproductive)
                .Permit(Activity.Historectomy, Health.NonReproductive)
                .PermitIf(Activity.HaveUnprotectedSex, Health.Pregnant, () => ParentsNotWatching);
            stateMachien
                .Configure(Health.Pregnant)
                .Permit(Activity.GiveBirth, Health.Reproductive);





        }
    }
}
