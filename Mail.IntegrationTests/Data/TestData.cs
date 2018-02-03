namespace Mail.IntegrationTests.Data
{
    public class TestData : IData
    {
        public int Value1 { get; }
        public string Value2 { get; }

        public TestData(int value1, string value2)
        {
            Value1 = value1;
            Value2 = value2;
        }

        public TestData(TestData _other)
        {
            Value1 = _other.Value1;
            Value2 = _other.Value2;
        }

        public override bool Equals(object obj)
        {
            var other = obj as TestData;

            if (other == null)
            {
                return false;
            }

            if (Value1 == other.Value1 &&
                Value2.Equals(other.Value2))
            {
                return true;
            }

            return false;
        }
    }
}
