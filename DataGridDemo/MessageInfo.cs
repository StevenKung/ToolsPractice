using System;

namespace DataGridDemo
{
	class MessageInfo
	{
		private DateTime m_Time;
		public DateTime Time { get => m_Time; }

		public MessageInfo()
		{
			m_Time = DateTime.Now;
		}
		public MessageType Type { get; set; } = MessageType.Info;
		public string Description { get; set; } = "Default Message";
	}

	public enum MessageType
	{
		Info,
		Warnning,
		Alarm
	}
}
