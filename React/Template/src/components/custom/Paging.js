import React, { Component } from 'react';
import { Pagination, PaginationItem, PaginationLink } from 'reactstrap';
import range from 'lodash.range';
import ChevronRightIcon from 'mdi-react/ChevronRightIcon';
import ChevronLeftIcon from 'mdi-react/ChevronLeftIcon';

//to call this component, call <Paging items={blogList} onChangePage={this.onChangePage} pageSize={5} initialPage={1} /> and pass in the item array list

export default class Paging extends Component {

  constructor(props) {
    super(props);
    this.state = { pager: {}, initialPage: 1, size: this.props.pageSize };
    this.setPage = this.setPage.bind(this);
    this.getPager = this.getPager.bind(this);
  }

  componentWillMount() {
    // set page if items array isn't empty
    if (this.props.items && this.props.items.length) {
      this.setPage(this.props.initialPage);
    }
  }

  componentDidUpdate(prevProps, prevState) {
    // reset page if items array has changed
    if (this.props.items !== prevProps.items) {
      this.setPage(this.props.initialPage);
    }
  }

  setPage(page) {
    const items = this.props.items;
    const pager = this.state.pager;
    const size = this.state.size;
 
    if (page < 1 || page > pager.totalPages) {
      return;
    }

    // get new pager object for specified page
    this.pager = this.getPager(items.length, page, size);
    console.log(this.pager);

    // get new page of items from items array
    const pageOfItems = items.slice(pager.startIndex, pager.endIndex + 1);
    console.log("startIndex: " + this.pager.startIndex);
    console.log("endIndex: " + this.pager.endIndex);
    console.log(items.slice(this.pager.startIndex, this.pager.endIndex + 1));

    // update state
    this.setState({ pager: this.pager });

    // call change page function in parent component
    //console.log(pageOfItems);
    // this.props.onChangePage(pageOfItems);
    this.props.onChangePage(items.slice(this.pager.startIndex, this.pager.endIndex + 1));
  }

  getPager(totalItems, currentPage, pageSize) {
    // default to first page
    currentPage = currentPage || 1;

    // default page size is 10
    pageSize = pageSize || 10;

    // calculate total pages
    let totalPages = Math.ceil(totalItems / pageSize);

    let startPage, endPage;
    if (totalPages <= 10) {
      // less than 10 total pages so show all
      startPage = 1;
      endPage = totalPages;
    } else {
      // more than 10 total pages so calculate start and end pages
      if (currentPage <= 6) {
        startPage = 1;
        endPage = 10;
      } else if (currentPage + 4 >= totalPages) {
        startPage = totalPages - 9;
        endPage = totalPages;
      } else {
        startPage = currentPage - 5;
        endPage = currentPage + 4;
      }
    }

    // calculate start and end item indexes
    let startIndex = (currentPage - 1) * pageSize;
    // let endIndex = Math.min(startIndex + pageSize - 1, totalItems - 1);
    let endIndex = (currentPage - 1) * pageSize + pageSize -1;

    // create an array of pages to ng-repeat in the pager control
    let pages = range(startPage, endPage + 1);

    // return object with all pager properties required by the view
    return {
      totalItems: totalItems,
      currentPage: currentPage,
      pageSize: pageSize,
      totalPages: totalPages,
      startPage: startPage,
      endPage: endPage,
      startIndex: startIndex,
      endIndex: endIndex,
      pages: pages
    };
  }

  render() {
   
    return (
      <div>
        {this.pager === undefined ? '' :
          <div className='pagination__wrap'>
            {(!this.pager.pages || this.pager.pages.length <= 1) ? '' :
              <Pagination className='pagination'>
                <PaginationItem className='pagination__item' disabled={this.pager.currentPage === 1}>
                  <PaginationLink className='pagination__link' onClick={() => this.setPage(1)}>
                    <ChevronLeftIcon className='pagination__link-icon' />
                  </PaginationLink>
                </PaginationItem>
                {this.pager.pages.map((page, index) =>
                  <PaginationItem className='pagination__item' key={index} active={this.pager.currentPage === page}>
                    <PaginationLink className='pagination__link'  onClick={() => this.setPage(page)}>
                      {page}
                    </PaginationLink>
                  </PaginationItem>
                )}
                <PaginationItem className='pagination__item' disabled={this.pager.currentPage === this.pager.totalPages}>
                  <PaginationLink className='pagination__link' onClick={() => this.setPage(this.pager.totalPages)}>
                    <ChevronRightIcon className='pagination__link-icon' />
                  </PaginationLink>
                </PaginationItem>
              </Pagination>
            }
            <div className='pagination-info'>
              <span>Showing {this.pager.pageSize * (this.pager.currentPage - 1) + 1 + ' '}
                to {this.pager.pageSize * this.pager.currentPage > this.props.items.length ? this.props.items.length
                  : this.pager.pageSize * this.pager.currentPage} of {this.props.items.length} items</span>
            </div>
          </div>
        }
      </div>
    );
  }
}

