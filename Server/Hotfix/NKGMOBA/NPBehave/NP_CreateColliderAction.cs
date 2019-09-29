//------------------------------------------------------------
// Author: 烟雨迷离半世殇
// Mail: 1778139321@qq.com
// Data: 2019年9月29日 12:29:40
//------------------------------------------------------------

using System.Numerics;
using Box2DSharp.Collision.Shapes;
using ETHotfix;
using ETModel;

namespace EThotfix
{
    [Event(EventIdType.CreateCollider)]
    public class NP_CreateColliderAction: AEvent<long, long, long>
    {
        /// <summary>
        /// 创建一次一定要同步一次
        /// </summary>
        /// <param name="a">unitID</param>
        /// <param name="b">数据结点载体ID</param>
        /// <param name="c">数据ID</param>
        public override void Run(long a, long b, long c)
        {
            Unit unit = Game.Scene.GetComponent<UnitComponent>().Get(a);
            B2S_HeroColliderData heroColliderData = unit.GetComponent<B2S_HeroColliderDataManagerComponent>()
                    .CreateHeroColliderData(unit, b, c);
            
            heroColliderData.m_Unit.Position = unit.Position;
            heroColliderData.m_Unit.Rotation = unit.Rotation;
            heroColliderData.SetColliderBodyTransform();

            //下面这一部分是Debug用的，稳定后请去掉
            {
                //广播碰撞体信息
                foreach (var VARIABLE in heroColliderData.m_Body.FixtureList)
                {
                    switch (VARIABLE.ShapeType)
                    {
                        case ShapeType.Polygon: //多边形
                            M2C_B2S_Debugger_Polygon test = new M2C_B2S_Debugger_Polygon() { Id = unit.Id, SustainTime = 2000, };
                            foreach (var VARIABLE1 in ((PolygonShape) VARIABLE.Shape).Vertices)
                            {
                                Vector2 worldPoint = heroColliderData.m_Body.GetWorldPoint(VARIABLE1);
                                test.Vects.Add(new M2C_B2S_VectorBase() { X = worldPoint.X, Y = worldPoint.Y });
                            }

                            MessageHelper.Broadcast(test);
                            break;
                        case ShapeType.Circle: //圆形
                            CircleShape myShape = (CircleShape) VARIABLE.Shape;
                            M2C_B2S_Debugger_Circle test1 = new M2C_B2S_Debugger_Circle()
                            {
                                Id = unit.Id,
                                SustainTime = 2000,
                                Radius = myShape.Radius,
                                Pos = new M2C_B2S_VectorBase()
                                {
                                    X = heroColliderData.m_Body.GetWorldPoint(myShape.Position).X,
                                    Y = heroColliderData.m_Body.GetWorldPoint(myShape.Position).Y
                                },
                            };
                            MessageHelper.Broadcast(test1);
                            //Log.Info($"是圆形，并且已经朝客户端发送绘制数据,半径为{myShape.Radius}");
                            break;
                    }
                }
            }
        }
    }
}