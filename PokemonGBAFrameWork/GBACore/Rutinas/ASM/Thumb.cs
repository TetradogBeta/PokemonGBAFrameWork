﻿/*
 * Creado por SharpDevelop.
 * Autor: Nintenlord
 * Sacado del proyecto AwesomeMapEditor-old/Dependencies/Hacking/GBA/GBA ASM editor/Thumb.cs
 * Fecha: 24/05/2017
 * Hora: 19:10
 * Licencia GNU GPL V3
 * Para cambiar esta plantilla use Herramientas | Opciones | Codificación | Editar Encabezados Estándar
 */
using System;
using System.Collections.Generic;
using System.Text;
using Gabriel.Cat.S.Extension;
namespace PokemonGBAFrameWork
{
    //añadir en un futuro un compilador!! tener en cuenta los comentarios,las variables,los ifs :)
	/// <summary>
	/// Decompila codigo maquina GBA a Thumb
	/// </summary>
	public static class Thumb
	{
        
        const char ENTERLINUX = '\n';
        const string ENTER = "\r\n";

        public static readonly Creditos Creditos;

        public static readonly char[] CaracteresComentario = { '\'', '@', '\\','*' };//si hay más añadir más

		static string[] decVals = new string[16]
		{
			"0","1","2","3","4","5","6","7","8",
			"9","10","11","12","13","14","15"
		};

		static string[] regs = new string[16]
		{
			"r0","r1","r2","r3","r4","r5","r6","r7",
			"r8","r9","r10","r11","r12","sp","lr","pc"
		};

		static string[] conditions = new string[16]
		{
			"eq","ne","cs","cc","mi","pl","vs","vc",
			"hi","ls","ge","lt","gt","le","","nv"
		};

		static string[] shifts = new string[5]
		{
			"lsl","lsr","asr","ror","rrx"
		};

		static string[] armMultLoadStore = new string[12]
		{
			// non-stack
			"da","ia","db","ib",
			// stack store
			"ed","ea","fd","fa",
			// stack load
			"fa","fd","ea","ed"
		};

		struct Opcode
		{
			public uint mask;
			public uint cval;
			public string mnemonic;

			public Opcode(uint mask, uint cval, string mnemonic)
			{
				this.mask = mask;
				this.cval = cval;
				this.mnemonic = mnemonic;
			}
		};

		static Opcode[] thumbOpcodes = new Opcode[]
		{
			// Format 1
			new Opcode (0xf800, 0x0000, "lsl %r0, %r3, %o"),
			new Opcode (0xf800, 0x0800, "lsr %r0, %r3, %o"),
			new Opcode (0xf800, 0x1000, "asr %r0, %r3, %o"),
			// Format 2
			new Opcode (0xfe00, 0x1800, "add %r0, %r3, %r6"),
			new Opcode (0xfe00, 0x1a00, "sub %r0, %r3, %r6"),
			new Opcode (0xfe00, 0x1c00, "add %r0, %r3, %i"),
			new Opcode (0xfe00, 0x1e00, "sub %r0, %r3, %i"),
			// Format 3
			new Opcode (0xf800, 0x2000, "mov %r8, %O"),
			new Opcode (0xf800, 0x2800, "cmp %r8, %O"),
			new Opcode (0xf800, 0x3000, "add %r8, %O"),
			new Opcode (0xf800, 0x3800, "sub %r8, %O"),
			// Format 4
			new Opcode (0xffc0, 0x4000, "and %r0, %r3"),
			new Opcode (0xffc0, 0x4040, "eor %r0, %r3"),
			new Opcode (0xffc0, 0x4080, "lsl %r0, %r3"),
			new Opcode (0xffc0, 0x40c0, "lsr %r0, %r3"),
			new Opcode (0xffc0, 0x4100, "asr %r0, %r3"),
			new Opcode (0xffc0, 0x4140, "adc %r0, %r3"),
			new Opcode (0xffc0, 0x4180, "sbc %r0, %r3"),
			new Opcode (0xffc0, 0x41c0, "ror %r0, %r3"),
			new Opcode (0xffc0, 0x4200, "tst %r0, %r3"),
			new Opcode (0xffc0, 0x4240, "neg %r0, %r3"),
			new Opcode (0xffc0, 0x4280, "cmp %r0, %r3"),
			new Opcode (0xffc0, 0x42c0, "cmn %r0, %r3"),
			new Opcode (0xffc0, 0x4300, "orr %r0, %r3"),
			new Opcode (0xffc0, 0x4340, "mul %r0, %r3"),
			new Opcode (0xffc0, 0x4380, "bic %r0, %r3"),
			new Opcode (0xffc0, 0x43c0, "mvn %r0, %r3"),
			// Format 5
			new Opcode (0xff80, 0x4700, "bx %h36"),
			new Opcode (0xfcc0, 0x4400, "[ ??? ]"),
			new Opcode (0xff00, 0x4400, "add %h07, %h36"),
			new Opcode (0xff00, 0x4500, "cmp %h07, %h36"),
			new Opcode (0xff00, 0x4600, "mov %h07, %h36"),
			// Format 6
			new Opcode (0xf800, 0x4800, "ldr %r8, =%J"),
			// Format 7
			new Opcode (0xfa00, 0x5000, "str%b %r0, [%r3, %r6]"),
			new Opcode (0xfa00, 0x5800, "ldr%b %r0, [%r3, %r6]"),
			// Format 8
			new Opcode (0xfe00, 0x5200, "strh %r0, [%r3, %r6]"),
			new Opcode (0xfe00, 0x5600, "ldrh %r0, [%r3, %r6]"),
			new Opcode (0xfe00, 0x5a00, "ldsb %r0, [%r3, %r6]"),
			new Opcode (0xfe00, 0x5e00, "ldsh %r0, [%r3, %r6]"),
			// Format 9
			new Opcode (0xe800, 0x6000, "str%B %r0, [%r3, %p]"),
			new Opcode (0xe800, 0x6800, "ldr%B %r0, [%r3, %p]"),
			// Format 10
			new Opcode (0xf800, 0x8000, "strh %r0, [%r3, %e]"),
			new Opcode (0xf800, 0x8800, "ldrh %r0, [%r3, %e]"),
			// Format 11
			new Opcode (0xf800, 0x9000, "str %r8, [sp, %w]"),
			new Opcode (0xf800, 0x9800, "ldr %r8, [sp, %w]"),
			// Format 12
			new Opcode (0xf800, 0xa000, "add %r8, pc, %w (=%K)"),
			new Opcode (0xf800, 0xa800, "add %r8, sp, %w"),
			// Format 13
			new Opcode (0xff00, 0xb000, "add sp, %s"),
			// Format 14
			new Opcode (0xffff, 0xb500, "push {lr}"),
			new Opcode (0xff00, 0xb400, "push {%l}"),
			new Opcode (0xff00, 0xb500, "push {%l,lr}"),
			new Opcode (0xffff, 0xbd00, "pop {pc}"),
			new Opcode (0xff00, 0xbd00, "pop {%l,pc}"),
			new Opcode (0xff00, 0xbc00, "pop {%l}"),
			// Format 15
			new Opcode (0xf800, 0xc000, "stmia %r8!, {%l}"),
			new Opcode (0xf800, 0xc800, "ldmia %r8!, {%l}"),
			// Format 17
			new Opcode (0xff00, 0xdf00, "swi %m"),
			// Format 16
			new Opcode (0xf000, 0xd000, "b%c %W"),
			// Format 18
			new Opcode (0xf800, 0xe000, "b %a"),
			// Format 19
			new Opcode (0xf800, 0xf000, "bl %A"),
			new Opcode (0xf800, 0xf800, "blh %Z"),
			new Opcode (0xff00, 0xbe00, "bkpt %O"),
			// Unknown
			new Opcode (0x0000, 0x0000, "[ ??? ]")
		};
		#region ARMopcodes

		static Opcode[] armOpcodes = new Opcode[]
		{
			// Undefined
			new Opcode (0x0e000010, 0x06000010, "[ undefined ]"),
			// Branch instructions
			new Opcode (0x0ff000f0, 0x01200010, "bx%c %r0"),
			new Opcode (0x0f000000, 0x0a000000, "b%c %o"),
			new Opcode (0x0f000000, 0x0b000000, "bl%c %o"),
			new Opcode (0x0f000000, 0x0f000000, "swi%c %q"),
			// PSR transfer
			new Opcode (0x0fbf0fff, 0x010f0000, "mrs%c %r3, %p"),
			new Opcode (0x0db0f000, 0x0120f000, "msr%c %p, %i"),
			// Multiply instructions
			new Opcode (0x0fe000f0, 0x00000090, "mul%c%s %r4, %r0, %r2"),
			new Opcode (0x0fe000f0, 0x00200090, "mla%c%s %r4, %r0, %r2, %r3"),
			new Opcode (0x0fa000f0, 0x00800090, "%umull%c%s %r3, %r4, %r0, %r2"),
			new Opcode (0x0fa000f0, 0x00a00090, "%umlal%c%s %r3, %r4, %r0, %r2"),
			// Load/Store instructions
			new Opcode (0x0fb00ff0, 0x01000090, "swp%c%b %r3, %r0, [%r4]"),
			new Opcode (0x0fb000f0, 0x01000090, "[ ??? ]"),
			new Opcode (0x0c100000, 0x04000000, "str%c%b%t %r3, %a"),
			new Opcode (0x0c100000, 0x04100000, "ldr%c%b%t %r3, %a"),
			new Opcode (0x0e100090, 0x00000090, "str%c%h %r3, %a"),
			new Opcode (0x0e100090, 0x00100090, "ldr%c%h %r3, %a"),
			new Opcode (0x0e100000, 0x08000000, "stm%c%m %r4%l"),
			new Opcode (0x0e100000, 0x08100000, "ldm%c%m %r4%l"),
			// Data processing
			new Opcode (0x0de00000, 0x00000000, "and%c%s %r3, %r4, %i"),
			new Opcode (0x0de00000, 0x00200000, "eor%c%s %r3, %r4, %i"),
			new Opcode (0x0de00000, 0x00400000, "sub%c%s %r3, %r4, %i"),
			new Opcode (0x0de00000, 0x00600000, "rsb%c%s %r3, %r4, %i"),
			new Opcode (0x0de00000, 0x00800000, "add%c%s %r3, %r4, %i"),
			new Opcode (0x0de00000, 0x00a00000, "adc%c%s %r3, %r4, %i"),
			new Opcode (0x0de00000, 0x00c00000, "sbc%c%s %r3, %r4, %i"),
			new Opcode (0x0de00000, 0x00e00000, "rsc%c%s %r3, %r4, %i"),
			new Opcode (0x0de00000, 0x01000000, "tst%c%s %r4, %i"),
			new Opcode (0x0de00000, 0x01200000, "teq%c%s %r4, %i"),
			new Opcode (0x0de00000, 0x01400000, "cmp%c%s %r4, %i"),
			new Opcode (0x0de00000, 0x01600000, "cmn%c%s %r4, %i"),
			new Opcode (0x0de00000, 0x01800000, "orr%c%s %r3, %r4, %i"),
			new Opcode (0x0de00000, 0x01a00000, "mov%c%s %r3, %i"),
			new Opcode (0x0de00000, 0x01c00000, "bic%c%s %r3, %r4, %i"),
			new Opcode (0x0de00000, 0x01e00000, "mvn%c%s %r3, %i"),
			// Coprocessor operations
			new Opcode (0x0f000010, 0x0e000000, "cdp%c %P, %N, %r3, %R4, %R0%V"),
			new Opcode (0x0e100000, 0x0c000000, "stc%c%L %P, %r3, %A"),
			new Opcode (0x0f100010, 0x0e000010, "mcr%c %P, %N, %r3, %R4, %R0%V"),
			new Opcode (0x0f100010, 0x0e100010, "mrc%c %P, %N, %r3, %R4, %R0%V"),
			// Unknown
			new Opcode (0x00000000, 0x00000000, "[ ??? ]")
		};
		#endregion

		class BL
		{
			ushort instruction;
			public ushort Instruction
			{
				get { return instruction; }
			}

			public BL(int pc, int offset)
			{
				int relativeOffset = offset - pc - 4;
				relativeOffset &= ~3;
				//0xF000
				relativeOffset >>= 11;
				instruction = (ushort)(0xF000 + relativeOffset);
			}
		}
		class BLH
		{
			ushort instruction;
			public ushort Instruction
			{
				get { return instruction; }
			}

			public BLH(int pc, int offset)
			{
				int relativeOffset = offset - pc - 4;
				relativeOffset &= ~3;
				//0xF800
				instruction = (ushort)(0xF800 + ((~0xF800) & relativeOffset));
			}
		}
		
        static Thumb()
        {
            Creditos = new Creditos();
            Creditos.Add("Romhacking", "Nintenlord", "Todo el código fuente de decompilar");
        }
		/// <summary>
		/// Convierte el código máquina en código ASM
		/// </summary>
		/// <param name="machinaCode">array con el código máquina</param>
		/// <param name="offset">inicio</param>
		/// <param name="length">si es inferior a -1 cogerá el tamaño de la array a decopilar</param>
		/// <returns>devuelve todas las lineas de codigo que contiene el codigoMaquina</returns>
		public static  IList<string> Disassemble(byte[] gbaBytes, int offset=0, int length=-1)
		{
			if(length<0)
				length=gbaBytes.Length;
			if(offset+length>gbaBytes.Length)
				throw new ArgumentOutOfRangeException();

			List<string> lines = new List<string>();
			StringBuilder result=new StringBuilder();
			ushort opcode;
			int index;
			int ii ;
			int amount;
			int lastFirstReg;
			bool hasRegs;
			int add;
			int regsa;
			int val;
			int reg;
			int nopcode;
			
			unsafe{
				fixed (byte* ptBytesGBA = gbaBytes)
				{
					for (int i = offset,f=length + offset; i < f; i += 2)
					{
						
						opcode = ((ushort*)ptBytesGBA)[i >> 1];
						index = 0;
						while ((thumbOpcodes[index].mask & opcode) != thumbOpcodes[index].cval)
							index++;

						ii = 0;
						while (ii < thumbOpcodes[index].mnemonic.Length)
						{
							if (thumbOpcodes[index].mnemonic[ii] != '%')
								result.Append( thumbOpcodes[index].mnemonic[ii++]);
							else
							{
								ii++;
								switch (thumbOpcodes[index].mnemonic[ii])
								{
									case 'r': //for register
										regsa = Convert.ToInt32(thumbOpcodes[index].mnemonic.Substring(ii + 1,1));
										result.Append( regs[(opcode >> regsa) & 7]);
										ii += 2;
										break;
									case 'o': //5 bit immediate value
										result.Append( "#0x");
										
										val = (opcode >> 6) & 0x1f;
										result .Append( Convert.ToString(val, 16));
										
										ii++;
										break;
									case 'p': //5 bit immediate value for ldr and str codes
										result.Append( "#0x");
										{
											val = (opcode >> 6) & 0x1f;
											if ((opcode & (1 << 12)) == 0)
												val <<= 2;
											result .Append( Convert.ToString(val, 16));
										}
										ii++;
										break;
									case 'e':
										result.Append( "#0x");
										result.Append( Convert.ToString(((opcode >> 6) & 0x1f) << 1, 16));
										ii++;
										break;
									case 'i':
										result.Append( "#0x");
										result.Append( Convert.ToString((opcode >> 6) & 7, 16));
										ii++;
										break;
									case 'h': //high registers, first is register bits, second high bit
										{
											reg = (opcode >> Convert.ToInt32(thumbOpcodes[index].mnemonic.Substring(ii + 1, 1))) & 7;
											if ((opcode & (1 << Convert.ToInt32(thumbOpcodes[index].mnemonic.Substring(ii + 2,1)))) != 0)
												reg += 8;
											result.Append( regs[reg]);
										}
										ii += 3;
										break;
									case 'O': //8 bit immediate
										result.Append( "#0x");
										result.Append(Convert.ToString(opcode & 0xFF, 16));
										ii++;
										break;
									case 'I':
										result.Append( "$");
										result.Append( Convert.ToString((i & 0xfffffffc) + 4 + ((opcode & 0xff) << 2), 16));
										ii++;
										break;
									case 'J':
										result.Append( "$");
										result.Append(Convert.ToString(((uint*)ptBytesGBA)[((i & 0xfffffffc)) + 4 + ((opcode & 0xff) << 2) >> 2], 16));;
										ii++;
										break;
									case 'K':
										result.Append( "$");
										result.Append( Convert.ToString((i & 0xfffffffc) + 4 + ((opcode & 0xff) << 2), 16));
										ii++;
										break;
									case 'b':
										if ((opcode & (1 << 10)) != 0)
											result.Append( "b");
										ii++;
										break;
									case 'B':
										if ((opcode & (1 << 12)) != 0)
											result.Append( "b");
										ii++;
										break;
									case 'w':
										result.Append( "#0x");
										result.Append( Convert.ToString((opcode & 0xff) << 2, 16));
										ii++;
										break;
									case 'W':
										{
											add = opcode & 0xFF;
											if ((add & 0x80) != 0)
												add = (int)((long)add - 0x80000000);

											result.Append( "$");
											result.Append( Convert.ToString(i - (i & 1) + (4 + (add << 1)),16));
										}
										ii++;
										break;
									case 'c':
										result.Append(conditions[(opcode >> 8) & 0xF]);
										ii++;
										break;
									case 's':
										result.Append( "#");
										if ((opcode & (1 << 7)) != 0)
											result.Append( "-");
										result.Append( "0x");
										result.Append( Convert.ToString((opcode & 0x7f) << 2, 16));
										ii++;
										break;
									case 'l': //register list, rewrite
										{
											amount = 0;
											lastFirstReg = 0;
											hasRegs = false;
											for (int u = 0; u < 8; u++)
											{
												if ((opcode & (1 << u)) != 0)
												{
													if (amount == 0)
														lastFirstReg = u;
													amount++;
												}
												else
												{
													if (hasRegs && amount > 0)
													{
														result.Append( ",");
														hasRegs = false;
													}

													if (amount > 2)
													{
														result.Append( regs[lastFirstReg] + "-" + regs[u]);
														hasRegs = true;
													}
													else if (amount == 2)
													{
														result .Append( regs[lastFirstReg] + "," + regs[u]);
														hasRegs = true;
													}
													else if (amount == 1)
													{
														result.Append( regs[u-1]);
														hasRegs = true;
													}
													amount = 0;
												}
											}
											if (amount != 0)
											{
												if (hasRegs)
													result.Append( ",");

												if (amount > 2){
													result.Append( regs[lastFirstReg] );
													result.Append( "-");
													result.Append( regs[7]);
												}
												else if (amount == 2){
													result.Append( regs[lastFirstReg]);
													result.Append( ",");
													result.Append( regs[7]);
												}
												else if(amount == 1)
													result.Append( regs[7]);
											}
										}
										ii++;
										break;
									case 'm':
										result.Append("$");
										result.Append( Convert.ToString(opcode & 0xff));
										ii++;
										break;
									case 'Z':
										result.Append( "$");
										result.Append( Convert.ToString((opcode & 0x7ff) << 1));
										ii++;
										break;
									case 'a':
										{
											add = opcode & 0x07ff;
											if ((add & 0x400) != 0)
												add = (int)((long)add - 0x80000000);
											add <<= 1;
											result.Append( "$");
											result.Append( Convert.ToString(i + add + 4, 16));
										}
										ii++;
										break;
									case 'A':
										{
											i += 2;
											nopcode = ((ushort*)ptBytesGBA)[(i) >> 1];
											add = opcode & 0x7ff;
											if ((add & 0x400) != 0)
												add |= 0xfff800;
											add = (add << 12) | ((nopcode & 0x7ff) << 1);
											result.Append( "$");
											result.Append( Convert.ToString(i + 2 + add, 16));
										}
										ii++;
										break;
									
								}
							}
						}


						lines.Add(result.ToString());
                        result.Clear();
                    }
				}
			}
			return lines;
		}

		private static string parseToString(ushort opcode, Opcode opcodes, uint offset)
		{
			string result = "";
			
			return result;
		}

        public static byte[] Assembler(string asmCode)
        {
            string[] lineasAsmCode;
            if(Environment.OSVersion.Platform==PlatformID.Unix)
            {
                 lineasAsmCode= asmCode.Split(ENTERLINUX);

            }
            else
            {
                lineasAsmCode = asmCode.Split(ENTER);
            }
            return Assembler(lineasAsmCode);
        }

        public static byte[] Assembler(string[] lineasAsmCode)
        {
            List<byte> bytesCompilados = new List<byte>();
            LimpiaLasLineas(lineasAsmCode);
            for(int i=0;i<lineasAsmCode.Length;i++)
            {
                if(!string.IsNullOrEmpty(lineasAsmCode[i]))
                {
                    //falta la parte que trabaja...
                }
            }
            return bytesCompilados.ToArray();
        }

        private static void LimpiaLasLineas(string[] lineasAsmCode)
        {
            StringBuilder[] strsLineasALimpiar = new StringBuilder[lineasAsmCode.Length];
            int indexAux;
            for (int i = 0; i < lineasAsmCode.Length; i++)
                strsLineasALimpiar[i] = new StringBuilder(lineasAsmCode[i]);
            //las limpio

            //quito los comentarios
            for (int j = 0; j < strsLineasALimpiar.Length; j++)
            {
                indexAux = lineasAsmCode[j].IndexOfAny(CaracteresComentario);
                strsLineasALimpiar[j].Remove(indexAux, lineasAsmCode[j].Length - indexAux);
            }
            //si hay que limpiar más añadir a partir de aqui
            //una vez limpios los reemplazo en la array
            for (int i = 0; i < lineasAsmCode.Length; i++)
                lineasAsmCode[i] = strsLineasALimpiar[i].ToString().ToLower();
        }
    }
}
