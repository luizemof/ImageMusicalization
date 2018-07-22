using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI.KMeans
{
    public partial class UKMeans : UserControl
    {
        public UKMeans()
        {
            InitializeComponent();
            _CentersCount = 7;
        }

        private int _CentersCount;
        private Dictionary<int, NumericUpDown> _DicCentersX = new Dictionary<int,NumericUpDown>();
        private Dictionary<int, NumericUpDown> _DicCentersY = new Dictionary<int,NumericUpDown>();

        /// <summary>
        /// Método que inicializa a classe, criando os controles necessários.
        /// </summary>
        public void Initialize()
        {
            TableLayoutPanel tblPanel;
            List<Control> controls;
            GroupBox groupBox;
            for (int i = 0; i < _CentersCount; i++)
            {
                tblPanel = _CreateTableLayout(i);
                controls = _CreateCenterControl(i);
               
                for (int j = 0; j < controls.Count; j++)
                    tblPanel.Controls.Add(controls[j], j, 0);

                groupBox = _CreateGroupBox(string.Format("LabelCenter{0}", i), string.Format("Centro {0}", i));
                groupBox.Controls.Add(tblPanel);

                tableLayout.Controls.Add(groupBox, 1, i);
            }
        }

        private GroupBox _CreateGroupBox(string name, string text)
        {
            GroupBox groupBox = new GroupBox();
            groupBox.AutoSize = true;
            groupBox.Name = name;
            groupBox.Text = text;
            groupBox.Dock = DockStyle.Fill;

            return groupBox;
        }

        private List<Control> _CreateCenterControl(int index)
        {
            List<Control> controls = new List<Control>();
            Label label;
            NumericUpDown numeric = new NumericUpDown();

            // Coordenada X
            label = _CreateLabel(string.Format("LabelCenterX{0}", index), "X");
            label.Padding = new System.Windows.Forms.Padding(3, 5, 3, 3);
            numeric = _CreateNumeric(string.Format("NumericX{0}", index));
            controls.Add(label);
            controls.Add(numeric);

            if (!_DicCentersX.ContainsKey(index))
                _DicCentersX.Add(index, numeric);
            else
                _DicCentersX[index] = numeric;

            // Coordenada Y
            label = _CreateLabel(string.Format("LabelCenterY{0}", index), "Y");
            label.Padding = new System.Windows.Forms.Padding(3, 5, 3, 3);
            numeric = _CreateNumeric(string.Format("NumericY{0}", index));
            controls.Add(label);
            controls.Add(numeric);

            if (!_DicCentersY.ContainsKey(index))
                _DicCentersY.Add(index, numeric);
            else
                _DicCentersY[index] = numeric;

            return controls;
        }

        private NumericUpDown _CreateNumeric(string name)
        {
            NumericUpDown numeric = new NumericUpDown();
            numeric.AutoSize = true;
            numeric.Minimum = 0;
            numeric.Dock = DockStyle.Fill;
            numeric.Name = name;
            numeric.Maximum = 999999M;

            return numeric;
        }

        private TableLayoutPanel _CreateTableLayout(int index)
        {
            TableLayoutPanel tblPanel = new TableLayoutPanel();
            tblPanel.ColumnCount = 4;
            tblPanel.RowCount = 1;
            tblPanel.AutoSize = true;
            tblPanel.Dock = DockStyle.Fill;
            tblPanel.Name = string.Format("tableLayoutPanel{0}", index);

            return tblPanel;
        }

        public Label _CreateLabel(string name, string text)
        {
            Label label = new Label();
            label.Name = name;
            label.Text = text;
            label.Dock = DockStyle.Fill;
            label.AutoSize = true;

            return label;
        }

        public List<Point> GetCenters()
        {
            List<Point> centers = new List<Point>();
            
            for (int i = 0; i < _CentersCount; i++)
            {
                if (!_DicCentersX.ContainsKey(i) || !_DicCentersY.ContainsKey(i))
                    throw new ArgumentException();

                centers.Add
                (
                    new Point()
                    {
                        X = (int)_DicCentersX[i].Value,
                        Y = (int)_DicCentersY[i].Value
                    }
                );
            }

            return centers;
        }
    }
}
