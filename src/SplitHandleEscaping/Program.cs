// See https://aka.ms/new-console-template for more information
using System.Text;

List<String> tests = new() {
    @"abc.abc\.abc",
    "jfdkslajf;sda.fdjska;fjdsa..jfdka;jfkl;as",
    @"['SYSTEM\\CurrentControlSet\\services\\CertSvc\\Configuration\\My\.CA\\PolicyModules\\CertificateAuthority_MicrosoftDefault\.Policy']"
};

foreach (string value in tests) {
    Console.WriteLine($"Current value: {value}");
    String[] parts = SplitHandleEscape(value, '.');
    Console.WriteLine("--------");
    for (Int32 i=0; i<parts.Length; i++) {
        Console.WriteLine($"Element {i + 1}: {parts[i]}");
    }

    Console.WriteLine();
}

static String[] SplitHandleEscape(String value, char delimiter) {
    List<StringBuilder> builders = new() {new()};
    Boolean inEscapeSequence = false;
    Int32 j = 0;
    for (Int32 i = 0; i < value.Length; i++) {
        if (inEscapeSequence) {
            if (value[i] == delimiter) {
                builders[j].Append(delimiter);
            } else {
                builders[j].Append($@"\{value[i]}");
            }
            inEscapeSequence = false;
            continue;
        }
        if (value[i] == '\\' && i < value.Length - 1) {
            inEscapeSequence = true;
            continue;
        }
        if (value[i] == delimiter) {
            builders.Add(new());
            j++;
            continue;
        }
        builders[j].Append(value[i]);
    }
    var payload = new String[builders.Count];
    for (Int32 i = 0; i < builders.Count; i++) {
        payload[i] = builders[i].ToString();
    }

    return payload;
}
