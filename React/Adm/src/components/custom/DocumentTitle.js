// use this for template V2 if Helmet support is not possible
// import DocumentTitle from 'react-document-title';
// export default DocumentTitle;

// only pick this or the above
import React, { Fragment } from 'react';
import PropTypes from 'prop-types';
import { Helmet } from 'react-helmet-async';

const DocumentTitle = ({ children, title }) => {
    return (
    <Fragment>
        <Helmet>
            <title>{title}</title>
        </Helmet>
        {children}
    </Fragment>);
}

DocumentTitle.propTypes = {
    children: PropTypes.node,
    title: PropTypes.string,
};

export default DocumentTitle;