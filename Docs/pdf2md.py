from langchain_community.document_loaders import PDFMinerPDFasHTMLLoader
from langchain_community.document_loaders import PyMuPDFLoader
from bs4 import BeautifulSoup, element
import fitz
import re
import io
import os
import glob

def scan_files(directory: str, extension: str) -> list:
    """Scan for files in a directory with a specific extension."""
    # Ensure the extension starts with a dot
    if not extension.startswith('.'):
        extension = '.' + extension

    # Use glob to scan for files
    files = glob.glob(os.path.join(directory, '*' + extension))

    return files

def clean_para(content: str):
    content = re.sub(r'\r|\n', ' ', content)
    content = re.sub(r'\t', ' ', content)
    content = re.sub(r' +', ' ', content)
    return content

def write_2_md(in_file: str, out_file: str):
    #only if it is PDFMiner
    loader = PDFMinerPDFasHTMLLoader(in_file)
    data = loader.load()[0]   # entire PDF is loaded as a single Document
    soup = BeautifulSoup(data.page_content,'html.parser')
    #with open("output/Volume1GettingStarted.html", "w", encoding='utf-8') as file:
    #    pass
    #    file.write(str(soup))
    with open(out_file, "w", encoding='utf-8') as file:
        content = soup.find_all('div')
        font_sizes = []
        header = ''
        detail = ''
        c: element.Tag
        for c in content:
            span = c.find('span')
            if not span: continue
            style = span.get('style')
            if not style: continue
            text = c.text
            footer = re.findall('1999-2015 robocoder corporation. All rights reserved', text)
            if footer and len(footer) > 0: continue
            fs = re.findall('font-size:(\d+)px', style)
            if fs and len(fs) > 0:
                fs = int(fs[0])
                if fs < 11: continue
                ff = re.findall('font-family:(.*);', style)
                if fs > 12:
                    if detail: 
                        detail = '\n##\n' + clean_para(detail)
                        #print(detail)
                        file.write(detail)
                        detail = ''
                    #if not header: header = '#'
                    header = header + ' ' + text
                else:
                    if header: 
                        header = '\n#\n' + clean_para(header)
                        #print(header)
                        file.write(header)
                        header = ''
                    #if not detail: detail = '##'
                    detail = detail + ' ' + text
            #if fs not in font_sizes: font_sizes.append(fs)
        if header: 
            header = '\n#\n' + clean_para(header)
            #print(header) 
            file.write(header)
        if detail: 
            detail = '\n##\n' + clean_para(detail)
            #print(detail)
            file.write(detail)

directory = '../Web/help'
extension = '.pdf'
docs = scan_files(directory, extension)

# Assume 'files' is a list of file paths
for file_path in docs:
    # Get the base name of the file (i.e., the filename with its extension, without the directory component)
    base_name = os.path.basename(file_path)
    
    # Split the base name into the filename and extension
    file_name, extension = os.path.splitext(base_name)
    
    #print(f"File Path: {file_path}")
    #print(f"Base Name: {base_name}")
    #print(f"File Name: {file_name}")
    #print(f"Extension: {extension}")
    write_2_md(file_path,f"output/{file_name}.md")
#for pymuPDF
#loader = PyMuPDFLoader("example_data/Volume1GettingStarted.pdf")
# data = loader.load()   # entire PDF is loaded as a single Document
# for  d in data:
#     p = d.load_page()
#     print(d.page_content)

#with open("output/Volume1GettingStarted.html", "w", encoding='utf-8') as file:
#    file.write(str(soup))
