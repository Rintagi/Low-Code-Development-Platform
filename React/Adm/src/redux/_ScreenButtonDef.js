export default {
    MstList: {
      MoreButton: { label: 'Master List More Button holder', iconClassName: 'fa fa-ellipsis-h icon-holder', className: 'mw-50', buttonType: 'More', visible: true, order: 1, expose: true },
      DropdownMenuButtons: {
        NewButton: { label: 'Add New Master', iconClassName: 'fa fa-plus', className: 'dropdown-button-blue', buttonType: 'New', visible: true, order: 2, expose: true },
        ExpTxtButton: { label: 'Export List', iconClassName: 'fa fa-file-text mr-10', className: 'dropdown-button-blue', buttonType: 'ExpTxt', visible: false, order: 4, expose: false },
        CopyButton: { label: 'Copy Master Record', iconClassName: 'fa fa-clipboard mr-10', className: 'dropdown-button-blue', buttonType: 'Copy', visible: true, order: 3, expose: false },
        DeleteButton: { label: 'Delete Master Record', iconClassName: 'fa fa-trash mr-10', className: 'dropdown-button-red', buttonType: 'Delete', visible: true, order: 5, expose: false },
        // InsRowButton: { label: 'Add Detail Record', iconClassName: 'fa fa-plus mr-10', className: 'dropdown-button-blue', buttonType: 'InsRow', visible: false, order: 6, expose: false },
        DrillDownButton: { label: 'View Detail List', iconClassName: 'fa fa-list-ul mr-10', className: 'dropdown-button-blue', buttonType: 'DrillDown', visible: false, order: 1, expose: false },
      },
      RowButtons: {
        DrillDownButton: { label: 'View Detail List', iconClassName: 'fa fa-list-ul', exposedClassName: 'edit-icon', className: 'dropdown-button-blue', buttonType: 'DrillDown', visible: true, order: 2, expose: false },
        InsRowButton: { label: 'Add', labelLong: 'Add New Item', iconClassName: 'fa fa-plus', exposedClassName: 'edit-icon', className: 'dropdown-button-blue', buttonType: 'InsRow', visible: true, order: 1, expose: false },
        EditHdrButton: { label: 'View Master Record', iconClassName: 'fa fa-eye', exposedClassName: 'edit-icon', className: 'dropdown-button-blue', buttonType: 'EditHdr', visible: true, order: 3, expose: false },
        CopyButton: { label: 'Copy Master Record', iconClassName: 'fa fa-clipboard', exposedClassName: 'edit-icon', className: 'dropdown-button-blue', buttonType: 'Copy', visible: false, order: 4, expose: false },
        DeleteButton: { label: 'Delete Master Record', iconClassName: 'fa fa-trash', exposedClassName: 'edit-icon', className: 'dropdown-button-red', buttonType: 'Delete', visible: true, order: 5, expose: false },
      },
      BottomButtons: {
        InsRowButton: { label: 'Add Detail Record', buttonType: 'InsRow', visible: true, order: 1, expose: true },
      }
    },
    Mst: {
      DropdownMenuButtons: {
        SaveCloseButton: { label: 'Save&Close Master', iconClassName: 'fa fa-envelope mr-10', className: 'dropdown-button-blue', buttonType: 'SaveClose', visible: true, order: 2, expose: true },
        CopyButton: { label: 'Copy Master Record', iconClassName: 'fa fa-clipboard mr-10', className: 'dropdown-button-blue', buttonType: 'Copy', visible: true, order: 3, expose: false },
        PrintButton: { label: 'Print Master Record', iconClassName: 'fa fa-print mr-10', className: 'dropdown-button-blue', buttonType: 'Print', visible: false, order: 4, expose: false },
        DeleteButton: { label: 'Delete Master Record', iconClassName: 'fa fa-trash mr-10', className: 'dropdown-button-red', buttonType: 'Delete', visible: true, order: 5, expose: false },
        InsRowButton: { label: 'Add Detail Record', iconClassName: 'fa fa-plus mr-10', className: 'dropdown-button-blue', buttonType: 'InsRow', visible: false, order: 6, expose: false },
        DrillDownButton: { label: 'View Detail List', iconClassName: 'fa fa-list-ul mr-10', className: 'dropdown-button-blue', buttonType: 'DrillDown', visible: false, order: 1, expose: false },
      },
      BottomButtons: {
        SaveButton: { label: 'Save', visible: true, buttonType: 'Save', order: 1, expose: true },
        SaveCloseButton: { label: 'Save&Close Master', buttonType: 'SaveClose', visible: true, order: 2, expose: true },
        DrillDownButton: { label: 'View Detail Record', buttonType: 'DrillDown', visible: false, order: 3, expose: true },
        DeleteButton: { label: 'Delete Master Record', buttonType: 'Delete', visible: false, order: 4, expose: false },
        CopyButton: { label: 'Copy Master Record', buttonType: 'Copy', visible: false, order: 5, expose: true },
      }
    },
    DtlList: {
      DropdownMenuButtons: {
        CopyRowButton: { label: 'Copy Detail Record', visible: true, iconClassName: 'fa fa-clipboard mr-10', className: 'dropdown-button-blue', buttonType: 'CopyRow', order: 1, expose: false },
        DelRowButton: { label: 'Delete Detail Record', visible: true, iconClassName: 'fa fa-trash mr-10', className: 'dropdown-button-red', buttonType: 'DelRow', order: 2, expose: false },
        InsRowButton: { label: 'Add', labelLong: 'Add Detail', buttonType: 'InsRow', visible: true, iconClassName: 'fa fa-plus', className: 'dropdown-button-blue', order: 3, expose: true },
      },
      RowButtons: {
        DelRowButton: { label: 'Delete Detail Record', visible: true, iconClassName: 'fa fa-trash', exposedClassName: 'edit-icon', className: 'dropdown-button-red', buttonType: 'DelRow', order: 3, expose: false },
        CopyRowButton: { label: 'Copy Detail Record', visible: true, iconClassName: 'fa fa-clipboard', exposedClassName: 'edit-icon', className: 'dropdown-button-blue', buttonType: 'CopyRow', order: 2, expose: false },
        EditRowButton: { label: 'View Detail Record', visible: true, iconClassName: 'fa fa-eye', exposedClassName: 'edit-icon', className: 'dropdown-button-blue', buttonType: 'EditRow', order: 1, expose: false },
      },
      BottomButtons: {
        InsRowButton: { label: 'Add Detail Record', visible: true, buttonType: 'InsRow', order: 1, expose: true },
        DelRowButton: { label: 'Delete Detail Record', visible: false, buttonType: 'DelRow', order: 3, expose: true },
        CopyRowButton: { label: 'Copy Detail Record', visible: false, buttonType: 'CopyRow', order: 5, expose: true },
      }
    },
    Dtl: {
      DropdownMenuButtons: {
        CopyRowButton: { label: 'Copy Detail Record', visible: true, iconClassName: 'fa fa-clipboard mr-10', className: 'dropdown-button-blue', buttonType: 'CopyRow', order: 1, expose: false },
        DelRowButton: { label: 'Delete Detail Record', visible: true, iconClassName: 'fa fa-trash mr-10', className: 'dropdown-button-red', buttonType: 'DelRow', order: 2, expose: false },
      },
      BottomButtons: {
        NewSaveDtlButton: { label: 'Save & New Detail Record', buttonType: 'NewSaveDtl', order: 1, visible: false, expose: true },
//        SaveDtlButton: { label: 'Save', buttonType: 'SaveDtl', order: 2, visible: true, expose: true },
//        SaveCloseDtlButton: { label: 'Save & Close Detail', buttonType: 'SaveCloseDtl', visible: false, order: 3, expose: true },
          SaveButton: { label: 'Save', buttonType: 'Save', order: 2, visible: true, expose: true },
          DelRowButton: { label: 'Delete Detail', buttonType: 'DelRow', visible: false, order: 4, expose: true },
      }
    }
  }