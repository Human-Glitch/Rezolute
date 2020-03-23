using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Systems
{
    public static class Consts
    {
        public struct ObjectTags
        {
            public const string PLAYER = "Player";
            public const string ENEMY = "Enemy";
            public const string BOSS = "Boss";
            public const string BLUE_SCANNER = "Blue Scanner";
            public const string RED_SCANNER = "Red Scanner";
        }

        public struct WordCloudTags
        {
            public const string PLAYER_CLOUD = "PlayerCloud";
            public const string ENEMY_CLOUD = "EnemyCloud";
            public const string MESSAGE_CLOUD = "messageCloud";
        }

    }
}
