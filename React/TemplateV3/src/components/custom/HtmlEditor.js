import React, { Component } from 'react';
import { EditorState, convertToRaw, ContentState } from 'draft-js';
import { Editor } from 'react-draft-wysiwyg';
import draftToHtml from 'draftjs-to-html';
import htmlToDraft from 'html-to-draftjs';
import classNames from 'classnames';

//To Use this component, call <HtmlEditor htmlContent="<div>this is testing</div>" />

export default class HtmlEditor extends Component {
  constructor(props) {
    super(props);
    this.state = {
      editorState: EditorState.createEmpty(),
      htmlContent: this.props.htmlContent,
      showHtml: false
    };

    // const html = '';
    const contentBlock = htmlToDraft(this.state.htmlContent);
    if (contentBlock) {
      const contentState = ContentState.createFromBlockArray(contentBlock.contentBlocks);
      this.state.editorState = EditorState.createWithContent(contentState);
      //this.state.htmlContent = html;
    }

    this.onEditorStateChange = this.onEditorStateChange.bind(this);
    this.onHtmlChange = this.onHtmlChange.bind(this);
    this.showContent = this.showContent.bind(this);
    this.showHtml = this.showHtml.bind(this);    
  }

  showContent() {
    this.setState({
      showHtml: false
    });
  };

  showHtml() {
    this.setState({
      showHtml: true
    });
  };

  onEditorStateChange(editorState) {
    console.log(editorState);
    this.setState({
      editorState: editorState,
      htmlContent: draftToHtml(convertToRaw(editorState.getCurrentContent()))
    });
  };

  onHtmlChange(htmlValue) {
    console.log(htmlValue.target.value);
    const contentBlock = htmlToDraft(htmlValue.target.value);
    if (contentBlock) {
      const contentState = ContentState.createFromBlockArray(contentBlock.contentBlocks);
      this.setState({ editorState: EditorState.createWithContent(contentState) })
    }

    this.setState({
      htmlContent: htmlValue.target.value
    });
  }

  uploadImageCallBack(value) {
    console.log(value);
  }

  render() {
    const { editorState } = this.state;
    return (
      <div>
        {!this.state.showHtml &&
          <Editor
            editorState={editorState}
            wrapperClassName="editor-wrapper"
            editorClassName="editor-content"
            toolbar={{
              image: {
                uploadCallback: this.uploadImageCallBack,
                alt: { present: true, mandatory: false },
                defaultSize: { height: 'auto', width: 'auto' },
                previewImage: true
              }
            }}
            onEditorStateChange={this.onEditorStateChange}
          />
        }
        {this.state.showHtml &&
          <div>
          <h4 className="htmlEdiTitle"><b>HTML</b></h4>
          <textarea
            className="editor-html"
            // disabled
            onChange={this.onHtmlChange}
            value={this.state.htmlContent}
          />
          </div>
        }
        <div role="toolbar" className="btn-toolbar">
          <div role="group" className="btn-group--icons btn-group">
            <button type="button" className={this.state.showHtml ? "btn btn-outline-secondary" : "btn btn-outline-secondary btn-active"} title="Content View" onClick={this.showContent}><i className="fa fa-file-text"></i></button>
            <button type="button" className={!this.state.showHtml ? "btn btn-outline-secondary" : "btn btn-outline-secondary btn-active"} title="Html View" onClick={this.showHtml}><i className="fa fa-code"></i></button>
          </div>
        </div>
      </div>
    );
  }
}

