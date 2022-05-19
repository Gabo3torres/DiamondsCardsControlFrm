using System;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Telerik.WinControls.Export;
//using Telerik.Windows.Documents;
using Telerik.WinControls.UI;

namespace DiamondsCardsControlFrm
{
    public partial class Form1 : Form
    {
        private UserTransversal user { get; set; }
        private DiamondsClient SelectedClient { get; set; }
        private ClientDiamondsRepository clientDiamonds { get; set; }
        private BankRepository _bankRepository { get; set; }
        private DescriptionCardRepository _descriptionCardRepository { get; set; }
        private string campoVacio { get; set; }
        public string ErrorCatcher { get; set; }

        GridViewSummaryItem gridViewSummaryItem = new GridViewSummaryItem();

        GridViewSummaryRowItem gridViewSummaryRowItem = new GridViewSummaryRowItem();

        public Form1(UserTransversal _user)
        {
            ErrorCatcher = "pasó la carga el form1";

            InitializeComponent();
            user = _user;
            LoadSources();
            //MessageBox.Show(ErrorCatcher);

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'diamondsDataSet.CreditCard' table. You can move, or remove it, as needed.
            this.creditCardTableAdapter.Fill(this.diamondsDataSet.CreditCard);
            // TODO: This line of code loads data into the 'diamondsDataSet.Invitation' table. You can move, or remove it, as needed.
            this.invitationTableAdapter.Fill(this.diamondsDataSet.Invitation);
            // TODO: This line of code loads data into the 'diamondsDataSet.CourtesyType' table. You can move, or remove it, as needed.
            this.courtesyTypeTableAdapter.Fill(this.diamondsDataSet.CourtesyType);
            // TODO: This line of code loads data into the 'diamondsDataSet.DescriptionCard' table. You can move, or remove it, as needed.
            this.descriptionCardTableAdapter.Fill(this.diamondsDataSet.DescriptionCard);
            _descriptionCardRepository = new DescriptionCardRepository();
            var bankCollection = _descriptionCardRepository.GetDescriptionCards().Select(x => x.Name).ToArray();
            salaLabelChange.Text = user.Sala.Nombre;
            DescTCtxt.AutoCompleteMode = AutoCompleteMode.Suggest;
            DescTCtxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            collection.AddRange(bankCollection);
            DescTCtxt.AutoCompleteCustomSource = collection;
            // TODO: This line of code loads data into the 'diamondsDataSet.SalaVIP' table. You can move, or remove it, as needed.
            this.salaVIPTableAdapter.Fill(this.diamondsDataSet.SalaVIP);
            if (user != null)
                if (user.Admin != true && user.RolId != 3)
                {
                    tabControl1.TabPages.Remove(tabPage2);
                    tabControl1.TabPages.Remove(tabPage3);
                    tabControl1.TabPages.Remove(tabPage4);
                    tabControl1.TabPages.Remove(tabPage5);
                    tabControl1.TabPages.Remove(tabPage6);
                    tabControl1.TabPages.Remove(CortesiaTab);
                    tabControl1.TabPages.Remove(DescripcionTabPage);
                    tabControl1.TabPages.Remove(CreditCard);

                    buttonCheck.Text = "Editar";

                }
                else if (user.RolId == 3)
                {
                    tabControl1.TabPages.Remove(tabPage2);
                    tabControl1.TabPages.Remove(tabPage3);
                    tabControl1.TabPages.Remove(tabPage4);
                    tabControl1.TabPages.Remove(tabPage5);
                    tabControl1.TabPages.Remove(tabPage6);
                    tabControl1.TabPages.Remove(CreditCard);
                    tabControl1.TabPages.Remove(CortesiaTab);
                    tabControl1.TabPages.Remove(DescripcionTabPage);
                    buttonCheck.Visible = false;
                    SaveBtn.Visible = false;
                    dateTimePicker1.Visible = true;
                    dateTimePicker1.Value = DateTime.Now;
                    dateTimePicker2.Value = DateTime.Now;
                    dateTimePicker2.Visible = true;
                    label16.Visible = true;
                    button1.Visible = true;
                    ViewButton.Visible = true;
                }
                else
                {
                    buttonCheck.Visible = true;
                    dateTimePicker1.Visible = true;
                    dateTimePicker1.Value = DateTime.Now;
                    dateTimePicker2.Value = DateTime.Now;
                    dateTimePicker2.Visible = true;
                    label16.Visible = true;
                    button1.Visible = true;
                    ViewButton.Visible = true;
                    DeleteButton.Visible = true;
                }
            // TODO: This line of code loads data into the 'diamondsDataSet.AereoLinea' table. You can move, or remove it, as needed.
            this.aereoLineaTableAdapter.Fill(this.diamondsDataSet.AereoLinea);
            // TODO: This line of code loads data into the 'diamondsDataSet.User' table. You can move, or remove it, as needed.
            this.userTableAdapter.Fill(this.diamondsDataSet.User);
            // TODO: This line of code loads data into the 'diamondsDataSet.Banco' table. You can move, or remove it, as needed.
            this.bancoTableAdapter.Fill(this.diamondsDataSet.Banco);
            this.radTimePicker1.Value = DateTime.Now.AddHours(4);
            this.ClearRegisterForm();

            GridViewSummaryItem gridViewSummaryItem = new GridViewSummaryItem();
            GridViewSummaryRowItem gridViewSummaryRowItem = new GridViewSummaryRowItem();

        }

        private void radClock1_Click(object sender, EventArgs e)
        {
            IngresoLbl.Text = DateTime.Now.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void radGridView1_Click(object sender, EventArgs e)
        {

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                MessageBox.Show(textBox2.Text, "mensaje", MessageBoxButtons.OK);

            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            this.bancoTableAdapter.Fill(this.diamondsDataSet.Banco);
        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorSaveButtom_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.bancoBindingSource.EndEdit();
            this.bancoTableAdapter.Update(this.diamondsDataSet.Banco);
            MessageBox.Show("Los cambios se han guardado correctamente", "Mensaje", MessageBoxButtons.OK);
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_Click(object sender, EventArgs e)
        {
            LoadSources();
        }

        private void LoadSources()
        {
            //ErrorCatcher = "Error en LoadSources";
            BankRepository banks = new BankRepository();
            AirLineRepository airLine = new AirLineRepository();
            ClientDiamondsRepository clientDiamonds = new ClientDiamondsRepository();
            AirLineCmbx.DataSource = airLine.GetAereoLineas();
            AirLineCmbx.DisplayMember = "Nombre";
            BankCmbx.DataSource = banks.GetBancos();
            BankCmbx.DisplayMember = "Nombre";

            //var selectedUser = users.ConsultarUsuario("Admin", "147852369");
            //NombreTxt.Text = selectedUser.Name;

            if (user.Admin != null && user.Admin == true)
            {
                radGridView1.DataSource = clientDiamonds.GetDiamondsClients().Where(x => DateTime.Parse(x.FechaIngreso.ToString()).Date >= DateTime.Now.AddDays(-1).Date).ToList().OrderByDescending(x => x.FechaIngreso);

                if (gridViewSummaryItem?.Name == null)
                {

                    gridViewSummaryItem.Name = "Id";
                    gridViewSummaryItem.Aggregate = GridAggregateFunction.Count;
                    gridViewSummaryRowItem.Add(gridViewSummaryItem);

                    this.radGridView1.SummaryRowsTop.Add(gridViewSummaryRowItem);
                }


                radGridView1.PageSize = 10;
            }
            else
            {
                radGridView1.DataSource = clientDiamonds.GetDiamondsClientsByRoom(user.Sala.Id).Where(x => DateTime.Parse(x.FechaIngreso.ToString()) >= DateTime.Now.AddHours(-13) && x.Supervisor.Contains(user.Name)).ToList().OrderByDescending(x => x.FechaIngreso);

                if (gridViewSummaryItem?.Name == null)
                {

                    gridViewSummaryItem.Name = "Id";
                    gridViewSummaryItem.Aggregate = GridAggregateFunction.Count;
                    gridViewSummaryRowItem.Add(gridViewSummaryItem);

                    this.radGridView1.SummaryRowsTop.Add(gridViewSummaryRowItem);
                }
                radGridView1.PageSize = 10;
                //MessageBox.Show(DateTime.Now.AddHours(-14).ToString());
                radGridView1.AllowEditRow = false;

                radGridView1.AllowDeleteRow = false;
                buttonCheck.Text = "Editar";
            }


        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.aereoLineaBindingSource.EndEdit();
            this.aereoLineaTableAdapter.Update(this.diamondsDataSet.AereoLinea);
            MessageBox.Show("Los cambios se han guardado correctamente", "Mensaje", MessageBoxButtons.OK);

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void userDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            var diamondsDataSetPrueba = new DiamondsDataSet();


            //this.userDataGridView.Columns[3]
            //var countUsers = userDataGridView.RowCount;
            //for(int i = 0; i < countUsers; i++)
            //{
            //    if(this.userDataGridView.Rows[i].Cells[7].Value == null)
            //    {
            //        this.userDataGridView.Rows[i].Cells[7].Value = false;
            //    }
            //}
            this.Validate();
            this.userBindingSource.EndEdit();
            if (this.diamondsDataSet.User.AdminColumn == null)
            { }//this.diamondsDataSet.User.AdminColumn = false;
            //diamondsDataSet = new DiamondsDataSet
            //{

            //};
            this.userTableAdapter.Update(this.diamondsDataSet.User);
            MessageBox.Show("Los cambios se han guardado correctamente", "Mensaje", MessageBoxButtons.OK);

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void radTimePicker1_Click(object sender, EventArgs e)
        {

        }

        private void radClock1_Click_1(object sender, EventArgs e)
        {
            IngresoLbl.Text = DateTime.Now.ToString();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Do you want to Close this window?", "Close Window", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
                this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (WindowState.Equals(FormWindowState.Maximized))
                this.WindowState = FormWindowState.Normal;

            else
            {

                this.WindowState = FormWindowState.Maximized;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (!WindowState.Equals(FormWindowState.Minimized))
            {
                this.WindowState = FormWindowState.Minimized;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clientDiamonds = new ClientDiamondsRepository();
            var nameASigned = string.Empty;
            int errorpos = 0;
            try
            {
                errorpos = 1;
                if (SelectedClient != null)

                {
                    errorpos = 2;
                    var selectedBank = BankCmbx.Text;
                    long Idbck = SelectedClient.Id;
                    if (user.Admin == true)
                    {
                        nameASigned = user.Name;
                        SelectedClient = new DiamondsClient()
                        {
                            //Supervisor = user.Name,
                            //FechaIngreso = DateTime.Parse(IngresoLbl.Text),
                            Nombre = NombreTxt.Text,
                            Observaciones = Observacionestxt.Text,
                            DocumentoHabilitante = Tarjetatxt.Text,
                            NoDelDocTarjetaCredito = Codigotxt.Text,
                            DescripcionTC = DescTCtxt.Text,
                            Vuelo = Vuelotxt.Text,
                            NoDelDocHabilVoucher = NoVouchertxt.Text,
                            //NumeroSala = user.Sala.Id,
                            Edad = EdadTextbox.Text,
                            Aerolinea = AirLineCmbx.Text,
                            Banco = selectedBank,
                            Aprobacion = Aprobaciontxt.Text,
                            Ciudad = ciudadTxt.Text,
                            DocIdentidad = taxId.Text
                            ,
                            NoInvitacionLineaAerea = NoInvitationtext.Text
                        ,
                            Id = Idbck

                        };
                        errorpos = 4;
                        var oldClient = clientDiamonds.GetClient(Idbck);
                        SelectedClient.FechaIngreso = oldClient.FechaIngreso;
                        SelectedClient.Supervisor = oldClient.Supervisor;
                        SelectedClient.NumeroSala = oldClient.NumeroSala;
                    }
                    else
                    {

                        SelectedClient = new DiamondsClient()
                        {
                            Supervisor = user.Name,
                            //FechaIngreso = DateTime.Parse(IngresoLbl.Text),
                            //Nombre = NombreTxt.Text,
                            Observaciones = Observacionestxt.Text,
                            //DocumentoHabilitante = Tarjetatxt.Text,
                            //NoDelDocTarjetaCredito = Codigotxt.Text,
                            DescripcionTC = DescTCtxt.Text,
                            Vuelo = Vuelotxt.Text,
                            NoDelDocHabilVoucher = NoVouchertxt.Text,
                            //NumeroSala = user.Sala.Id,
                            Edad = EdadTextbox.Text,
                            Aerolinea = AirLineCmbx.Text,
                            Banco = selectedBank,
                            Aprobacion = Aprobaciontxt.Text,
                            Ciudad = ciudadTxt.Text,
                            DocIdentidad = taxId.Text,
                            NoInvitacionLineaAerea = NoInvitationtext.Text
                        ,
                            Id = Idbck

                        };
                        errorpos = 5;

                        var oldClient = clientDiamonds.GetClient(Idbck);
                        SelectedClient.Nombre = oldClient.Nombre;
                        SelectedClient.FechaIngreso = oldClient.FechaIngreso;
                        SelectedClient.DocumentoHabilitante = oldClient.DocumentoHabilitante;
                        SelectedClient.NoDelDocTarjetaCredito = oldClient.NoDelDocTarjetaCredito;
                        SelectedClient.NumeroSala = oldClient.NumeroSala;

                    }
                    errorpos = 6;
                    if (clientDiamonds.UpdateClientRevisor(nameASigned, Idbck, SelectedClient))
                    {

                        if (user.Admin == true)
                        {
                            MessageBox.Show("Cliente Revisado", "Salida", MessageBoxButtons.OK);
                            radGridView1.DataSource = clientDiamonds.GetDiamondsClients().Where(x => DateTime.Parse(x.FechaIngreso.ToString()).Date >= DateTime.Now.AddDays(-1).Date).ToList().OrderByDescending(x => x.FechaIngreso);
                            //radGridView1.DataSource = clientDiamonds.GetDiamondsClients().ToList().OrderByDescending(x => x.FechaIngreso);
                            ClearRegisterForm();
                        }
                        else
                        {
                            MessageBox.Show("Cliente Corregido", "Salida", MessageBoxButtons.OK);
                            //radGridView1.DataSource = clientDiamonds.GetDiamondsClientsByRoom(user.Sala.Id).Where(x => DateTime.Parse(x.FechaIngreso.ToString()).Date == DateTime.Now.Date
                            //&& x.Supervisor.Contains(user.Name)).ToList().OrderByDescending(x => x.FechaIngreso);
                            radGridView1.DataSource = clientDiamonds.GetDiamondsClientsByRoom(user.Sala.Id).Where(x => DateTime.Parse(x.FechaIngreso.ToString()) >= DateTime.Now.AddHours(-13) && x.Supervisor.Contains(user.Name)).ToList().OrderByDescending(x => x.FechaIngreso);

                            ClearRegisterForm();
                        }
                        //radGridView1.DataSource = clientDiamonds.GetDiamondsClients().Where(x=> DateTime.Parse(x.FechaIngreso.ToString()).Date >= DateTime.Now.AddDays(-1).Date).ToList().OrderByDescending(x=>x.FechaIngreso);
                        //radGridView1.DataSource = clientDiamonds.GetDiamondsClients().ToList().OrderByDescending(x => x.FechaIngreso);
                        ClearRegisterForm();
                    }
                    else
                    {
                        MessageBox.Show("No se pudo registrar los cambios", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + errorpos.ToString());
            }

        }

        private void radGridView1_CurrentRowChanged(object sender, Telerik.WinControls.UI.CurrentRowChangedEventArgs e)
        {
            //if(string.IsNullOrEmpty(Codigotxt.Text) )
            //fillCurrentRowSelceted(this.radGridView1.CurrentRow);

            //else
            //{
            //   var result = MessageBox.Show("Desea cargar en el formulario los elementos de la celda seleccionada?", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            //    if(result == DialogResult.OK)
            //        fillCurrentRowSelceted(this.radGridView1.CurrentRow);

            //}
        }

        private void fillCurrentRowSelceted(GridViewRowInfo currentRow)
        {
            try
            {
                if (currentRow != null)
                {
                    this.NombreTxt.Text = this.GetSafeString(currentRow.Cells[6].Value);
                    this.Observacionestxt.Text = this.GetSafeString(currentRow.Cells[17].Value);
                    this.Codigotxt.Text = this.GetSafeString(currentRow.Cells[9].Value);
                    this.Tarjetatxt.Text = this.GetSafeString(currentRow.Cells[15].Value);
                    this.EdadTextbox.Text = this.GetSafeString(currentRow.Cells[7].Value);
                    // this.Tarjetatxt.Text = this.GetSafeString(currentRow.Cells[10].Value);
                    this.DescTCtxt.Text = this.GetSafeString(currentRow.Cells[16].Value);
                    this.NoVouchertxt.Text = this.GetSafeString(currentRow.Cells[10].Value);
                    this.Vuelotxt.Text = this.GetSafeString(currentRow.Cells[4].Value);
                    this.IngresoLbl.Text = this.GetSafeString(currentRow.Cells[2].Value);
                    this.InnTimePicker.Value = DateTime.Parse(this.GetSafeString(currentRow.Cells[2].Value));
                    var bancoDelCliente = this.GetSafeString(currentRow.Cells[8].Value);
                    this.BankCmbx.SelectedIndex = this.BankCmbx.FindStringExact(bancoDelCliente);
                    this.AirLineCmbx.SelectedIndex = this.AirLineCmbx.FindStringExact(this.GetSafeString(currentRow.Cells[3].Value));
                    this.taxId.Text = this.GetSafeString(currentRow.Cells[13].Value);
                    this.Aprobaciontxt.Text = this.GetSafeString(currentRow.Cells[11].Value);
                    this.ciudadTxt.Text = this.GetSafeString(currentRow.Cells[14].Value);
                    this.NoInvitationtext.Text = this.GetSafeString(currentRow.Cells[12].Value);
                    SelectedClient = new DiamondsClient()
                    {
                        Id = long.Parse(this.GetSafeString(currentRow.Cells[0].Value)),
                    };
                    //this.NoDocTctxt.Text = this.GetSafeString(currentRow.Cells[5].Value);
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message);
            }
        }

        private string GetSafeString(object value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            return value.ToString();
        }

        private void ClearRegisterForm()
        {

            this.NombreTxt.Text = string.Empty;
            this.Observacionestxt.Text = string.Empty;
            this.Codigotxt.Text = string.Empty;
            this.Tarjetatxt.Text = string.Empty;
            this.DescTCtxt.Text = string.Empty;
            this.NoVouchertxt.Text = string.Empty;
            this.Vuelotxt.Text = string.Empty;
            this.IngresoLbl.Text = "n/a";
            this.InnTimePicker.Value = DateTime.Now;
            this.EdadTextbox.Text = string.Empty;
            this.taxId.Text = string.Empty;
            this.ciudadTxt.Text = string.Empty;
            this.Aprobaciontxt.Text = string.Empty;
            this.checkBox1.CheckState = CheckState.Unchecked;
            this.AirLineCmbx.SelectedIndex = -1;
            this.BankCmbx.SelectedIndex = -1;
            this.NoInvitationtext.Text = string.Empty;
            SelectedClient = null;
            //this.NoDocTctxt.Text = string.Empty;
        }

        private void SaveBtn_Click_1(object sender, EventArgs e)
        {
            if (IngresoLbl.Text.Contains("n/a"))
            {
                MessageBox.Show("Por favor seleccione la hora de Ingreso", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!areFillallFields())
            {
                MessageBox.Show("El campo " + campoVacio + " está vacio", "Alerta");
            }
            else
            {
                var selectedBank = BankCmbx.Text;
                SelectedClient = new DiamondsClient()
                {
                    Supervisor = user.Name,
                    FechaIngreso = InnTimePicker.Value,
                    Nombre = NombreTxt.Text,
                    Observaciones = Observacionestxt.Text,
                    DocumentoHabilitante = Tarjetatxt.Text,
                    NoDelDocTarjetaCredito = Codigotxt.Text,
                    DescripcionTC = DescTCtxt.Text,
                    Vuelo = Vuelotxt.Text,
                    NoDelDocHabilVoucher = NoVouchertxt.Text,
                    ETD = DateTime.Parse(radTimePicker1.Value.ToString()),
                    NumeroSala = user.Sala.Nombre,
                    Edad = EdadTextbox.Text,
                    Aerolinea = AirLineCmbx.Text,
                    Banco = selectedBank,
                    Aprobacion = Aprobaciontxt.Text,
                    Ciudad = ciudadTxt.Text,
                    DocIdentidad = taxId.Text,
                    NoInvitacionLineaAerea = NoInvitationtext.Text,


                };
                bool validationClient = false;
                validationClient = validationClientLogic(SelectedClient);
                if (validationClient)
                {
                    clientDiamonds = new ClientDiamondsRepository();
                    try
                    {
                        var clientVip = clientDiamonds.Insert(SelectedClient);

                        if (clientVip != null)
                            MessageBox.Show("Se guardo satisfactoriamente al sr(a) " + clientVip.Nombre, "Ingreso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadSources();
                        ClearRegisterForm();

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("No se pudo guardar los datos Error: " + ex.Message);

                    }
                }


            }

        }

        private bool validationClientLogic(DiamondsClient cliente)
        {
            if (cliente.Banco.Contains("INVITACION") || cliente.NoDelDocTarjetaCredito.Contains("NN") /*|| !cliente.NoDelDocTarjetaCredito.Contains("00000")*/)
            {
                return true;
            }
            else
            {
                clientDiamonds = new ClientDiamondsRepository();
                var latesRegistereds = clientDiamonds.GetDiamondsClientByCreditCard(cliente.NoDelDocTarjetaCredito).ToList().OrderByDescending(x => x.FechaIngreso);
                if (latesRegistereds != null)
                {
                    var latesRegistered = latesRegistereds.FirstOrDefault();
                    if (latesRegistered != null)
                    {
                        TimeSpan? ts = cliente.FechaIngreso - latesRegistered.FechaIngreso;

                        string stringTotalHoras = ts.ToString();
                        TimeSpan totalHoras = TimeSpan.Parse(stringTotalHoras);

                        if (totalHoras.TotalHours < 6)
                        {
                            var messageResult = MessageBox.Show("El cliente estuvo en la sala " + latesRegistered.Sala + " hace " + totalHoras.TotalHours + " horas; Desea Ingresarlo?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                            if (messageResult == DialogResult.No)
                                return false;
                            return true;
                        }
                        if (DateTime.Parse(cliente.FechaIngreso.ToString()).Date == DateTime.Parse(latesRegistered.FechaIngreso.ToString()).Date)
                        {
                            var messageResult = MessageBox.Show("El cliente ya ha ingresado anteriormente en este dia; desea volverlo a ingresar?", "Cliente Ingresado", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (messageResult == DialogResult.No)
                            {
                                return false;
                                //MessageBox.Show("no graba");
                            }
                        }
                        //if (totalHoras.TotalHours < 24)
                        //{
                        //    var messageResult = MessageBox.Show("El cliente ya ha ingresado anteriormente en este dia; desea volverlo a ingresar?", "Cliente Ingresado", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        //    if (messageResult == DialogResult.No)
                        //    {
                        //        return false;
                        //        //MessageBox.Show("no graba");
                        //    }
                        //}
                        return true;
                    }
                    else
                    {
                        return true;
                    }
                }
                else
                {
                    return true;
                }
            }
        }

        private bool areFillallFields()
        {

            if (string.IsNullOrEmpty(NombreTxt.Text))
            {
                campoVacio = "Nombre";
                return false;
            }
            if (string.IsNullOrEmpty(Codigotxt.Text))
            {
                campoVacio = "Codigo";
                return false;
            }
            //if (string.IsNullOrEmpty(Tarjetatxt.Text))
            //{
            //    campoVacio = "tarjeta";
            //    return false;
            //}

            return true;
        }

        private string GetCreditCardtype(string CreditcardNumber)
        {
            Regex regVisa = new Regex("^4[0-9]{12}(?:[0-9]{3})?$");
            Regex regMaster = new Regex("^5[1-5][0-9]{14}$");
            Regex regExpress = new Regex("^3[47][0-9]{13}$"); //376654646623017
            Regex regDiners = new Regex("^3(?:0[0-5]|[68][0-9])[0-9]{11}$");
            Regex regDiscover = new Regex("^6(?:011|5[0-9]{2})[0-9]{12}$");
            Regex regJCB = new Regex("^(?:2131|1800|35\\d{3})\\d{11}$");

            if (regVisa.IsMatch(CreditcardNumber))

            {
                var _creditCardType = "VISA";
                CreditCardFillField(CreditcardNumber, _creditCardType);
                return _creditCardType;
            }
            if (regMaster.IsMatch(CreditcardNumber))
            {
                var _creditCardType = "MASTER";
                CreditCardFillField(CreditcardNumber, _creditCardType);
                return _creditCardType;
            }
            if (regExpress.IsMatch(CreditcardNumber))
            {
                var _creditCardType = "AMEX";
                CreditCardFillField(CreditcardNumber, _creditCardType);
                return _creditCardType;
            }
            if (regDiners.IsMatch(CreditcardNumber))
            {
                var _creditCardType = "DINERS";
                CreditCardFillField(CreditcardNumber, _creditCardType);
                return _creditCardType;
            }
            if (regDiscover.IsMatch(CreditcardNumber))
            {
                var _creditCardType = "DISCOVER";
                CreditCardFillField(CreditcardNumber, _creditCardType);
                return _creditCardType;
            }
            if (regJCB.IsMatch(CreditcardNumber))
            {
                var _creditCardType = "JCB";
                CreditCardFillField(CreditcardNumber, _creditCardType);
                return _creditCardType;
            }

            var creditCardType = "INVALID";
            CreditCardFillField(CreditcardNumber, creditCardType);
            return creditCardType;

        }


        private void CardReader_OTC(string CardReader)
        {
            bool CaretPresent = false;
            bool EqualPresent = false;
            bool AmpersandPresent = false;
            string PersonName;
            string CardNumber;
            string CardExpiration;
            CaretPresent = CardReader.Contains("^");
            EqualPresent = CardReader.Contains("=");
            AmpersandPresent = CardReader.Contains("&");

            if (CaretPresent)
            {
                string[] CardData = CardReader.Split('^');
                //B1234123412341234^CardUser/John^030510100000019301000000877000000?
                //%B5110540189197131&GABRIEL TORRES-           &21092212290_
                PersonName = CardData[1];
                CardNumber = CardData[0];
                CardExpiration = CardData[2].Substring(2, 2) + "/" + CardData[2].Substring(0, 2);
            }
            else if (EqualPresent)
            {
                string[] CardData = CardReader.Split('=');
                //1234123412341234=0305101193010877?

                CardNumber = CardData[0];
                CardExpiration = CardData[1].Substring(2, 2) + "/" + CardData[1].Substring(0, 2);
            }
            else if (AmpersandPresent)
            {
                string[] CardData = CardReader.Split('&');
                //B1234123412341234^CardUser/John^030510100000019301000000877000000?
                //%B5110540189197131&GABRIEL TORRES-           &21092212290_
                PersonName = CardData[1];
                char[] MyChar = { 'ñ', ' ', '-' };
                PersonName = PersonName.TrimEnd(MyChar);
                CardNumber = CardData[0].TrimEnd(MyChar);
                CardNumber = new string(CardNumber.Where(x => char.IsDigit(x)).ToArray());
                this.Codigotxt.Text = CardNumber;
                this.NombreTxt.Text = PersonName;
                CardExpiration = CardData[2].Substring(2, 2) + "/" + CardData[2].Substring(0, 2);
                //Console.WriteLine(GetCreditCardtype(CardNumber));
                //Console.WriteLine(PersonName);
            }
        }

        private void textBox2_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.CardReader_OTC(textBox2.Text);
                this.Tarjetatxt.Text = this.GetCreditCardtype(Codigotxt.Text);
                this.textBox2.Text = string.Empty;
                this.IngresoLbl.Text = DateTime.Now.ToString();
                this.InnTimePicker.Value = DateTime.Now;
                this.radTimePicker1.Value = DateTime.Now.AddHours(4);
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            ClearRegisterForm();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                edadLabel.Visible = Enabled;
                EdadTextbox.Visible = Enabled;
                Codigotxt.Text = "NN";
                Tarjetatxt.Text = "NN";
                IngresoLbl.Text = DateTime.Now.ToString();
            }
            else
            {
                edadLabel.Visible = false;
                EdadTextbox.Visible = false;
            }
        }

        private void CompareAlarm()
        {



        }

        private void button2_Click(object sender, EventArgs e)
        {
            ClearRegisterForm();
            try
            {
                var clientDiamondsnewDS = new ClientDiamondsRepository();
                RadGridView newPrintedRad = new RadGridView();
                if (user.Admin != null && user.Admin == true)
                {

                    this.radGridView1.DataSource = clientDiamondsnewDS.GetDiamondsClients().Where(x => DateTime.Parse(x.FechaIngreso.ToString()).Date >= dateTimePicker1.Value.Date && DateTime.Parse(x.FechaIngreso.ToString()).Date <= dateTimePicker2.Value.Date && x.Sala.Contains(user.Sala.Nombre)).ToList().OrderByDescending(x => x.FechaIngreso);
                    newPrintedRad.DataSource = clientDiamondsnewDS.GetDiamondsClients().Where(x => DateTime.Parse(x.FechaIngreso.ToString()).Date >= dateTimePicker1.Value.Date && DateTime.Parse(x.FechaIngreso.ToString()).Date <= dateTimePicker2.Value.Date && x.Sala.Contains(user.Sala.Nombre)).ToList().OrderByDescending(x => x.FechaIngreso);
                    this.radGridView1.DataSource = newPrintedRad.DataSource;
                }
                GridViewSpreadExport spreadExporter = new GridViewSpreadExport(radGridView1);
                //spreadExporter.AsyncExportProgressChanged+= spreadExporter_async
                SpreadExportRenderer exportRenderer = new SpreadExportRenderer();
                //radGridView1.DataSource = clientDiamondsnewDS.GetDiamondsClients().Where(x => DateTime.Parse(x.FechaIngreso.ToString()).Date >= DateTime.Now.AddDays(-1).Date).ToList().OrderByDescending(x => x.FechaIngreso);

                spreadExporter.RunExportAsync("C:\\VIPExcel\\" + DateTime.Now.ToString("yyyyMMdd") + user.ShortName + ".xlsx", exportRenderer);

                MessageBox.Show("El documento fue generado satisfactoriamente");
                if (user.Admin != null && user.Admin == true)
                {
                    radGridView1.DataSource = clientDiamondsnewDS.GetDiamondsClients().Where(x => DateTime.Parse(x.FechaIngreso.ToString()).Date >= DateTime.Now.AddDays(-1).Date).ToList().OrderByDescending(x => x.FechaIngreso);
                }
                else
                {
                    //radGridView1.DataSource = clientDiamondsnewDS.GetDiamondsClientsByRoom(user.Sala.Id).Where(x => DateTime.Parse(x.FechaIngreso.ToString()).Date == DateTime.Now.Date && 
                    //x.Supervisor.Contains(user.Name)).ToList().OrderByDescending(x => x.FechaIngreso);
                    radGridView1.DataSource = clientDiamondsnewDS.GetDiamondsClientsByRoom(user.Sala.Id).Where(x => DateTime.Parse(x.FechaIngreso.ToString()) >= DateTime.Now.AddHours(-13) &&
                    x.Supervisor.Contains(user.Name)).ToList().OrderByDescending(x => x.FechaIngreso);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("No se pudo generar el documento, vuelva a intentarlo " + ex.Message, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.salaVIPBindingSource.EndEdit();
            this.salaVIPTableAdapter.Update(this.diamondsDataSet.SalaVIP);
            MessageBox.Show("Los cambios se han guardado correctamente", "Mensaje", MessageBoxButtons.OK);

        }

        private void radGridView1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.descriptionCardBindingSource.EndEdit();
            this.descriptionCardTableAdapter.Update(this.diamondsDataSet.DescriptionCard);
            MessageBox.Show("Los cambios se han guardado correctamente", "Mensaje", MessageBoxButtons.OK);
            this.descriptionCardTableAdapter.Fill(this.diamondsDataSet.DescriptionCard);
            _descriptionCardRepository = new DescriptionCardRepository();
            var bankCollection = _descriptionCardRepository.GetDescriptionCards().Select(x => x.Name).ToArray();
            salaLabelChange.Text = user.Sala.Nombre;
            DescTCtxt.AutoCompleteMode = AutoCompleteMode.Suggest;
            DescTCtxt.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            collection.AddRange(bankCollection);
            DescTCtxt.AutoCompleteCustomSource = collection;
        }

        private void radGridView1_DataBindingComplete(object sender, GridViewBindingCompleteEventArgs e)
        {
            var maxString = string.Empty;
            var maxStringSolicitado = string.Empty;
            foreach (var row in radGridView1.Rows)
            {
                var value = row.Cells[2].Value.ToString();
                var valueSolicitado = row.Cells[4].Value.ToString();
                maxString = maxString.Length > value.Length ? maxString : value;
                maxStringSolicitado = maxStringSolicitado.Length > valueSolicitado.Length ? maxStringSolicitado : valueSolicitado;
            }


            var minWidth = radGridView1.CreateGraphics().MeasureString(maxString, radGridView1.Font);
            var minWidthSolicitado = radGridView1.CreateGraphics().MeasureString(maxStringSolicitado, radGridView1.Font);
            if (minWidth.Width > 0)
            {
                for (int i = 0; i < 20; i++)
                {
                    radGridView1.Columns[i].MinWidth = (int)minWidth.Width + 10;
                    radGridView1.Columns[i].MaxWidth = (int)minWidth.Width + 10;
                    radGridView1.Columns[i].Width = (int)minWidth.Width + 10;
                }
                if (minWidthSolicitado.Width > 0)
                {
                    radGridView1.Columns[0].MinWidth = (int)minWidthSolicitado.Width + 10;
                    radGridView1.Columns[0].MaxWidth = (int)minWidthSolicitado.Width + 10;
                    radGridView1.Columns[0].Width = (int)minWidthSolicitado.Width + 10;

                    radGridView1.Columns[4].MinWidth = (int)minWidthSolicitado.Width + 10;
                    radGridView1.Columns[4].MaxWidth = (int)minWidthSolicitado.Width + 10;
                    radGridView1.Columns[4].Width = (int)minWidthSolicitado.Width + 10;

                    radGridView1.Columns[7].MinWidth = (int)minWidthSolicitado.Width + 10;
                    radGridView1.Columns[7].MaxWidth = (int)minWidthSolicitado.Width + 10;
                    radGridView1.Columns[7].Width = (int)minWidthSolicitado.Width + 10;
                }
            }


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            clientDiamonds = new ClientDiamondsRepository();
            this.radGridView1.DataSource = clientDiamonds.GetDiamondsClients().Where(x => DateTime.Parse(x.FechaIngreso.ToString()).Date >= DateTime.Now.AddDays(-1).Date).ToList().OrderByDescending(x => x.FechaIngreso);
            this.dateTimePicker1.Value = DateTime.Now;
            this.dateTimePicker2.Value = DateTime.Now;
        }

        private void Codigotxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void BankCmbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BankCmbx.Text.Contains("INVITACION"))
            {
                if (string.IsNullOrEmpty(Codigotxt.Text))
                    Codigotxt.Text = "00000";
                IngresoLbl.Text = DateTime.Now.ToString();
                InnTimePicker.Value = DateTime.Now;
            }
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.courtesyTypeBindingSource.EndEdit();
            this.courtesyTypeTableAdapter.Update(this.diamondsDataSet.CourtesyType);
            MessageBox.Show("Los cambios se han guardado correctamente", "Mensaje", MessageBoxButtons.OK);
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.invitationBindingSource.EndEdit();
            this.invitationTableAdapter.Update(this.diamondsDataSet.Invitation);
            MessageBox.Show("Los cambios se han guardado correctamente", "Mensaje", MessageBoxButtons.OK);
        }

        private void radGridView1_CellClick(object sender, GridViewCellEventArgs e)
        {
            if (string.IsNullOrEmpty(Codigotxt.Text))
            {
                if (string.IsNullOrEmpty(NombreTxt.Text))
                    fillCurrentRowSelceted(this.radGridView1.CurrentRow);
                else
                {
                    var result = MessageBox.Show("Desea cargar en el formulario los elementos de la celda seleccionada?", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (result == DialogResult.OK)
                        fillCurrentRowSelceted(this.radGridView1.CurrentRow);

                }
            }
            else if (string.IsNullOrEmpty(NombreTxt.Text))
            {
                if (string.IsNullOrEmpty(Codigotxt.Text))
                    fillCurrentRowSelceted(this.radGridView1.CurrentRow);
                else
                {
                    var result = MessageBox.Show("Desea cargar en el formulario los elementos de la celda seleccionada?", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                    if (result == DialogResult.OK)
                        fillCurrentRowSelceted(this.radGridView1.CurrentRow);

                }
            }

            else
            {
                var result = MessageBox.Show("Desea cargar en el formulario los elementos de la celda seleccionada?", "Alert", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
                if (result == DialogResult.OK)
                    fillCurrentRowSelceted(this.radGridView1.CurrentRow);

            }
        }

        private void ViewButton_Click(object sender, EventArgs e)
        {
            ClearRegisterForm();
            var clientDiamondsnewDS = new ClientDiamondsRepository();
            RadGridView newPrintedRad = new RadGridView();
            newPrintedRad.DataSource = clientDiamondsnewDS.GetDiamondsClients().Where(x => DateTime.Parse(x.FechaIngreso.ToString()).Date >= dateTimePicker1.Value.Date && DateTime.Parse(x.FechaIngreso.ToString()).Date <= dateTimePicker2.Value.Date && x.Sala.Contains(user.Sala.Nombre)).ToList().OrderByDescending(x => x.FechaIngreso);
            this.radGridView1.DataSource = newPrintedRad.DataSource;
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            ClearRegisterForm();

        }


        private void CreditCardFillField(string CreditcardNumber, string creditCardType)
        {
            if (CreditcardNumber.Length > 11)
            {
                if (creditCardType.Equals("INVALID"))
                {
                    CreditCardRepository creditCard = new CreditCardRepository();
                    var y = creditCard.GetCreditCards().OrderByDescending(x => x.Numero.Length).ToList();
                    if (y.LongCount() < 1)
                    {
                        var selectedCreditCard = y.FirstOrDefault(x => CreditcardNumber.Substring(0, 8).Contains(x.Numero));
                        if (selectedCreditCard != null)
                        {
                            BankCmbx.Text = selectedCreditCard.Entidad;
                            DescTCtxt.Text = selectedCreditCard.Descripcion;
                        }
                    }
                }
                else
                {
                    CreditCardRepository creditCard = new CreditCardRepository();
                    var y = creditCard.GetCreditCards().Where(x => x.Tipo.Equals(creditCardType)).OrderByDescending(x => x.Numero.Length).ToList();
                    if (y.LongCount() < 1)
                    {
                        y = creditCard.GetCreditCards().OrderByDescending(x => x.Numero.Length).ToList();
                    }
                    var selectedCreditCard = y.FirstOrDefault(x => CreditcardNumber.Substring(0, 8).Contains(x.Numero));
                    if (selectedCreditCard != null)
                    {
                        BankCmbx.Text = selectedCreditCard.Entidad;
                        DescTCtxt.Text = selectedCreditCard.Descripcion;
                    }
                }
            }

        }

        private void toolStripButton19_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.creditCardBindingSource.EndEdit();
            this.creditCardTableAdapter.Update(this.diamondsDataSet.CreditCard);
            MessageBox.Show("Los cambios se han guardado correctamente", "Mensaje", MessageBoxButtons.OK);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            var response = MessageBox.Show("Desea eliminar el cliente seleccionado?", "Eliminar Cliente", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (response == DialogResult.Yes)
            {
                clientDiamonds = new ClientDiamondsRepository();
                var nameASigned = string.Empty;
                int errorpos = 0;
                try
                {
                    errorpos = 1;
                    if (SelectedClient != null)

                    {
                        errorpos = 2;
                        var selectedBank = BankCmbx.Text;
                        long Idbck = SelectedClient.Id;
                        if (user.Admin == true)
                        {
                            nameASigned = user.Name;
                            SelectedClient = new DiamondsClient()
                            {
                                //Supervisor = user.Name,
                                //FechaIngreso = DateTime.Parse(IngresoLbl.Text),
                                Nombre = NombreTxt.Text,
                                Observaciones = Observacionestxt.Text,
                                DocumentoHabilitante = Tarjetatxt.Text,
                                NoDelDocTarjetaCredito = Codigotxt.Text,
                                DescripcionTC = DescTCtxt.Text,
                                Vuelo = Vuelotxt.Text,
                                NoDelDocHabilVoucher = NoVouchertxt.Text,
                                //NumeroSala = user.Sala.Id,
                                Edad = EdadTextbox.Text,
                                Aerolinea = AirLineCmbx.Text,
                                Banco = selectedBank,
                                Aprobacion = Aprobaciontxt.Text,
                                Ciudad = ciudadTxt.Text,
                                DocIdentidad = taxId.Text
                                ,
                                NoInvitacionLineaAerea = NoInvitationtext.Text
                            ,
                                Id = Idbck

                            };
                            errorpos = 4;
                            var oldClient = clientDiamonds.GetClient(Idbck);
                            SelectedClient.FechaIngreso = oldClient.FechaIngreso;
                            SelectedClient.Supervisor = oldClient.Supervisor;
                            SelectedClient.NumeroSala = oldClient.NumeroSala;
                        }
                        errorpos = 6;
                        if (clientDiamonds.DeleteClient(nameASigned, Idbck, SelectedClient))
                        {

                            if (user.Admin == true)
                            {
                                MessageBox.Show("Cliente Eliminado", "Salida", MessageBoxButtons.OK);
                                radGridView1.DataSource = clientDiamonds.GetDiamondsClients().Where(x => DateTime.Parse(x.FechaIngreso.ToString()).Date >= DateTime.Now.AddDays(-1).Date).ToList().OrderByDescending(x => x.FechaIngreso);
                                //radGridView1.DataSource = clientDiamonds.GetDiamondsClients().ToList().OrderByDescending(x => x.FechaIngreso);
                                ClearRegisterForm();
                            }
                            ClearRegisterForm();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo registrar los cambios", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + errorpos.ToString());
                }

            }

        }

        private void tabPage6_Click(object sender, EventArgs e)
        {

        }
    }
}
