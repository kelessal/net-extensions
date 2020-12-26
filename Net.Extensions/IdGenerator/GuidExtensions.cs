using System;
using System.Collections.Generic;
using System.Text;

namespace Net.Extensions
{
	public static class GuidExtensions
	{

		public static string ToShortId(this Guid guid)
        {
			string encoded = Convert.ToBase64String(guid.ToByteArray());
			encoded = encoded
				.Replace("/", "_")
				.Replace("+", "-");
			return encoded.Substring(0, 22);
		}
	}

}
