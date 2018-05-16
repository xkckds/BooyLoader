using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BootLoader
{
    public class Command
    {
        // cmdastart 
        public static byte cmd_len = 13;
        // cmdastart 
        public const byte cmd_start = 0xAA;
        // cmdend
        const byte cmd_end1 = 0xCC;
        const byte cmd_end2 = 0x33;
        const byte cmd_end3 = 0xC3;
        const byte cmd_end4 = 0x3C;
        // page
        const byte page_welcome = 0x00;
        // operator
        const byte set_instruct = 0x01;
        const byte get_instruct = 0x02;
        // parameter
        const byte page_welcome_wel = 0x55;
        const byte page_welcome_upgrade = 0x01;

        public byte[] DSPShakehand = { cmd_start, 0x00, set_instruct, page_welcome, page_welcome_wel, 0x59, 0x58, 0x57, 0x56, cmd_end1, cmd_end2, cmd_end3, cmd_end4 }; // 握手
        public byte[] DSPUpgrade = { cmd_start, 0x00, set_instruct, page_welcome, page_welcome_upgrade, 0x59, 0x58, 0x57, 0x56, cmd_end1, cmd_end2, cmd_end3, cmd_end4 }; // 升级

        public byte[] CMDRev = { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 }; // 接收


    }
}
