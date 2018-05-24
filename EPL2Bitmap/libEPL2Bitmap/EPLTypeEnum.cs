namespace libEPL2Bitmap
{
    public partial class EPL2Bitmap
    {
        private enum EPLTypeEnum
        {
            String, // A (ASCII TEXT) String line
            Barcode, // B Barcode Line / b other image (e.g. QR code)
            Setting, // D / O
            NewLine, // N
            Format, // Q / q
            Quantity, // P
            Form, // F Manipulate stored Form (functions) SortedList<string,[string|bmp]>?
            Box, // X
            Line, // L
            Unknown = -1
        }

        private enum EPLFormFunctions
        {
            Information, // I List stored forms and information
            Store, // S Start a stored function
            Retrieve, // R Insert inline
            Delete,
            End, // End of stored function
            Unknown = -1
        }
        
        private enum EPLFormatEnum
        {
            
            Width, // X
            /// <summary>
            /// how long the label is (hiehgt)
            /// </summary>
            Length, // Y (Height)
            Gap, // Gap between each label
            /// <summary>
            /// How dark (usually how much heat to apply)
            /// </summary>
            Heat,
            Speed, // I dont think this matters
            Unknown = -1
        }

        private enum EPLReverseTypeEnum
        {
            WhiteOnBlack, // R
            BlackOnWhite, // N
            Unknown = -1
        }

        private static EPLTypeEnum GetEPLType(char type)
        {
            switch (type)
            {
                case 'A':
                    return EPLTypeEnum.String;
                case 'B':
                    return EPLTypeEnum.Barcode;
                case 'D':
                case 'O':
                    return EPLTypeEnum.Setting;
                case 'N':
                    return EPLTypeEnum.NewLine;
                case 'Q':
                case 'q':
                    return EPLTypeEnum.Format;
                case 'P':
                    return EPLTypeEnum.Quantity;
                case 'F':
                    return EPLTypeEnum.Form;
                case 'X':
                    return EPLTypeEnum.Box;
                case 'L':
                    return EPLTypeEnum.Line;
                default:
                    return EPLTypeEnum.Unknown;
            }
        }

        private static EPLFormFunctions GetEPLFunction(char type)
        {
            switch (type)
            {
                case 'I':
                    return EPLFormFunctions.Information;
                case 'S':
                    return EPLFormFunctions.Store;
                case 'R':
                    return EPLFormFunctions.Retrieve;
                case 'K':
                    return EPLFormFunctions.Delete;
                case 'E':
                    return EPLFormFunctions.End;
                default:
                    return EPLFormFunctions.Unknown;
            }
        }

        private static EPLReverseTypeEnum GetEPLReverseType(char type)
        {
            switch (type)
            {
                case 'N':
                    return EPLReverseTypeEnum.BlackOnWhite;
                case 'R':
                    return EPLReverseTypeEnum.WhiteOnBlack;
                default:
                    return EPLReverseTypeEnum.Unknown;
            }
        }
    }
}
